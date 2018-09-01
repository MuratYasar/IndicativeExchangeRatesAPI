using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IndicativeExchangeRates.Host.Helper
{
    internal sealed class GetExchangeRates
    {
        public static async System.Threading.Tasks.Task<IQueryable<IndicativeExchangeRates.Model.Currency>> GetData()
        {
            string responseBody = string.Empty;

            using (System.Net.Http.HttpResponseMessage response = await ServiceClient.GetHttpResponse())
            {
                if (response.IsSuccessStatusCode)
                {
                    using (System.Net.Http.HttpContent content = response.Content)
                    {
                        responseBody = await content.ReadAsStringAsync();
                    }                    
                }
                else
                {
                    Log.Logger.Instance.Error(new Exception($"UserName:{Authentication.AuthService.UserName} - Exchange rate data is not available. Response is {response.StatusCode.ToString()}"));
                    throw new Exception("No data available!");
                }
            }


            if (responseBody.EndsWith(@"</Tarih_Date"))
            {
                responseBody = responseBody.Replace("</Tarih_Date", "</Tarih_Date>");
            }

            System.Xml.Linq.XDocument document = System.Xml.Linq.XDocument.Parse(responseBody);

            var resultSet = (from result in document.Descendants("Currency")
                             
                             .Where(result => result.Descendants("Isim").Any())
                             .Where(result => result.Descendants("CurrencyName").Any())
                             .Where(result => result.LastAttribute != null)
                             .Where(result => result.Descendants("ForexBuying").Any())
                             .Where(result => result.Descendants("ForexSelling").Any())
                             .Where(result => result.Descendants("BanknoteBuying").Any())
                             .Where(result => result.Descendants("BanknoteSelling").Any())

                             select
                             new IndicativeExchangeRates.Model.Currency
                             {
                                 Tarih = document.Root.Attribute("Tarih").Value.Trim(),
                                 Date = document.Root.Attribute("Date").Value.Trim(),
                                 Bulten_No = document.Root.Attribute("Bulten_No").Value.Trim(),
                                 CrossOrder = result.Attribute("CrossOrder").Value.Trim(),
                                 Kod = result.Attribute("Kod").Value.Trim(),
                                 CurrencyCode = result.Attribute("CurrencyCode").Value.Trim(),
                                 Unit = Convert.ToByte(result.Element("Unit").Value.Trim()),
                                 Isim = result.Element("Isim").Value.Trim(),
                                 CurrencyName = result.Element("CurrencyName").Value.Trim(),
                                 ForexBuying = (!string.IsNullOrWhiteSpace(result.Element("ForexBuying").Value.Trim()) ? Convert.ToDecimal(result.Element("ForexBuying").Value.Trim()) : default(decimal?)),
                                 ForexSelling = (!string.IsNullOrWhiteSpace(result.Element("ForexSelling").Value.Trim()) ? Convert.ToDecimal(result.Element("ForexSelling").Value.Trim()) : default(decimal?)),
                                 BanknoteBuying = (!string.IsNullOrWhiteSpace(result.Element("BanknoteBuying").Value.Trim()) ? Convert.ToDecimal(result.Element("BanknoteBuying").Value.Trim()) : default(decimal?)),
                                 BanknoteSelling = (!string.IsNullOrWhiteSpace(result.Element("BanknoteSelling").Value.Trim()) ? Convert.ToDecimal(result.Element("BanknoteSelling").Value.Trim()) : default(decimal?)),
                                 CrossRateUSD = (!string.IsNullOrWhiteSpace(result.Element("CrossRateUSD").Value.Trim()) ? Convert.ToDecimal(result.Element("CrossRateUSD").Value.Trim()) : default(decimal?)),
                                 CrossRateOther = (!string.IsNullOrWhiteSpace(result.Element("CrossRateOther").Value.Trim()) ? Convert.ToDecimal(result.Element("CrossRateOther").Value.Trim()) : default(decimal?))
                             }).AsQueryable();

            return resultSet;

        }
    }
}
