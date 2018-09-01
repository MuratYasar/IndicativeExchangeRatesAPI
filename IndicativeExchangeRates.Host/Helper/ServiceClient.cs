using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IndicativeExchangeRates.Host.Helper
{
    internal sealed class ServiceClient
    {
        private static readonly HttpClient client;
        
        static ServiceClient()
        {
            client = new HttpClient();

            var uri = new Uri(@"http://www.tcmb.gov.tr/kurlar/today.xml");
            client = new HttpClient()
            {
                BaseAddress = uri
            };

            var sp = System.Net.ServicePointManager.FindServicePoint(uri);
            sp.ConnectionLeaseTimeout = (int)TimeSpan.FromMinutes(1).TotalMilliseconds;
            System.Net.ServicePointManager.DnsRefreshTimeout = (int)TimeSpan.FromMinutes(1).TotalMilliseconds;

        }

        public static async Task<HttpResponseMessage> GetHttpResponse()
        {
            return await client.GetAsync(client.BaseAddress);
        }
    }
}
