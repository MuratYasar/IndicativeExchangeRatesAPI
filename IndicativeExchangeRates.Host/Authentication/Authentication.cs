using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace IndicativeExchangeRates.Host.Authentication
{
    internal sealed class Authentication
    {
        private static readonly HttpClient client;
        
        public static string UserName { get; set; }
        public static string Password { get; set; }        
                
        static Authentication()
        {
            client = new HttpClient();

            #region Getting Authentication Uri From App.config file

            dynamic uriValue = null;

            if (System.Configuration.ConfigurationManager.AppSettings["authenticationUrl"] == null)
            {
                FileInfo fileInfo = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
                uriValue = (from c in XElement.Load(fileInfo.DirectoryName + "\\App.config").Elements("appSettings").Elements("add").Attributes("value") select c).FirstOrDefault().Value;
                fileInfo = null;
            }
            else
            {
                uriValue = System.Configuration.ConfigurationManager.AppSettings["authenticationUrl"].ToString();
            }

            #endregion Getting Authentication Uri From App.config file

            var uri = new Uri(uriValue);

            client = new HttpClient()
            {
                BaseAddress = uri
            };

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            var sp = System.Net.ServicePointManager.FindServicePoint(uri);
            sp.ConnectionLeaseTimeout = (int)TimeSpan.FromMinutes(1).TotalMilliseconds;
            System.Net.ServicePointManager.DnsRefreshTimeout = (int)TimeSpan.FromMinutes(1).TotalMilliseconds;
        }

        public static async Task<HttpResponseMessage> GetHttpResponse()
        {
            var byteArray = Encoding.ASCII.GetBytes($"{UserName}:{Password}");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            return await client.GetAsync(client.BaseAddress);
        }
    }
}

