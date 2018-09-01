using IndicativeExchangeRates.FilterSort;
using IndicativeExchangeRates.PluginContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IndicativeExchangeRates.Plugin.XML
{
    public class OutputXML : IPlugin
    {
        public string Name => this.GetType().Name;

        public string Description => "Produce XML Output";

        public async Task<object> GetAllData()
        {
            var response = await Host.ExchangeRateAPI.GetResult();

            XElement xml = new XElement("Tarih_Date",
                    from cur in response
                    select new XElement("Currency",
                        new XAttribute("CrossOrder", cur.CrossOrder),
                        new XAttribute("Kod", cur.Kod),
                        new XAttribute("CurrencyCode", cur.CurrencyCode),
                            new XElement("Unit", cur.Unit),
                            new XElement("Isim", cur.Isim),
                            new XElement("CurrencyName", cur.CurrencyName),
                            new XElement("ForexBuying", cur.ForexBuying),
                            new XElement("ForexSelling", cur.ForexSelling),
                            new XElement("BanknoteBuying", cur.BanknoteBuying),
                            new XElement("BanknoteSelling", cur.BanknoteSelling),
                            new XElement("CrossRateUSD", cur.CrossRateUSD),
                            new XElement("CrossRateOther", cur.CrossRateOther)
                        )
                );

            var s = xml.ToString();

            return s;
        }        

        public async Task<object> GetFilteredData(List<ExpressionFilter> filters)
        {
            var response = await Host.ExchangeRateAPI.GetFilteredResult(filters);

            XElement xml = new XElement("Tarih_Date",
                    from cur in response
                    select new XElement("Currency",
                        new XAttribute("CrossOrder", cur.CrossOrder),
                        new XAttribute("Kod", cur.Kod),
                        new XAttribute("CurrencyCode", cur.CurrencyCode),
                            new XElement("Unit", cur.Unit),
                            new XElement("Isim", cur.Isim),
                            new XElement("CurrencyName", cur.CurrencyName),
                            new XElement("ForexBuying", cur.ForexBuying ?? default(decimal?)),
                            new XElement("ForexSelling", cur.ForexSelling ?? default(decimal?)),
                            new XElement("BanknoteBuying", cur.BanknoteBuying ?? default(decimal?)),
                            new XElement("BanknoteSelling", cur.BanknoteSelling ?? default(decimal?)),
                            new XElement("CrossRateUSD", cur.CrossRateUSD ?? default(decimal?)),
                            new XElement("CrossRateOther", cur.CrossRateOther ?? default(decimal?))
                        )
                );

            var s = xml.ToString();

            return s;
        }

        public async Task<object> GetSortedData(List<ExpressionSort> sorts)
        {
            var response = await Host.ExchangeRateAPI.GetSortedResult(sorts);

            XElement xml = new XElement("Tarih_Date",
                    from cur in response
                    select new XElement("Currency",
                        new XAttribute("CrossOrder", cur.CrossOrder),
                        new XAttribute("Kod", cur.Kod),
                        new XAttribute("CurrencyCode", cur.CurrencyCode),
                            new XElement("Unit", cur.Unit),
                            new XElement("Isim", cur.Isim),
                            new XElement("CurrencyName", cur.CurrencyName),
                            new XElement("ForexBuying", cur.ForexBuying ?? default(decimal?)),
                            new XElement("ForexSelling", cur.ForexSelling ?? default(decimal?)),
                            new XElement("BanknoteBuying", cur.BanknoteBuying ?? default(decimal?)),
                            new XElement("BanknoteSelling", cur.BanknoteSelling ?? default(decimal?)),
                            new XElement("CrossRateUSD", cur.CrossRateUSD ?? default(decimal?)),
                            new XElement("CrossRateOther", cur.CrossRateOther ?? default(decimal?))
                        )
                );

            var s = xml.ToString();

            return s;
        }

        public async Task<object> GetFilteredAndSortedData(List<ExpressionFilter> filters, List<ExpressionSort> sorts)
        {
            var response = await Host.ExchangeRateAPI.GetFilteredAndSortedResult(filters, sorts);

            XElement xml = new XElement("Tarih_Date",
                    from cur in response
                    select new XElement("Currency",
                        new XAttribute("CrossOrder", cur.CrossOrder),
                        new XAttribute("Kod", cur.Kod),
                        new XAttribute("CurrencyCode", cur.CurrencyCode),
                            new XElement("Unit", cur.Unit),
                            new XElement("Isim", cur.Isim),
                            new XElement("CurrencyName", cur.CurrencyName),
                            new XElement("ForexBuying", cur.ForexBuying ?? default(decimal?)),
                            new XElement("ForexSelling", cur.ForexSelling ?? default(decimal?)),
                            new XElement("BanknoteBuying", cur.BanknoteBuying ?? default(decimal?)),
                            new XElement("BanknoteSelling", cur.BanknoteSelling ?? default(decimal?)),
                            new XElement("CrossRateUSD", cur.CrossRateUSD ?? default(decimal?)),
                            new XElement("CrossRateOther", cur.CrossRateOther ?? default(decimal?))
                        )
                );

            var s = xml.ToString();

            return s;
        }
    }
}
