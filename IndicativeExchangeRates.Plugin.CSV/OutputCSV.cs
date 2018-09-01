using IndicativeExchangeRates.FilterSort;
using IndicativeExchangeRates.PluginContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IndicativeExchangeRates.Plugin.CSV
{
    public class OutputCSV : IPlugin
    {
        public string Name => this.GetType().Name;

        public string Description => "Produce CSV Output";

        public async Task<object> GetAllData()
        {
            var response = await Host.ExchangeRateAPI.GetResult();

            StringBuilder sb = new StringBuilder();

            foreach (var item in ToCsv(response, "|", true))
            {
                sb.AppendLine(item);
            }

            return sb.ToString();
        }

        private static IEnumerable<string> ToCsv<T>(IEnumerable<T> objectlist, string separator = ",", bool header = true)
        {
            FieldInfo[] fields = typeof(T).GetFields();
            PropertyInfo[] properties = typeof(T).GetProperties();
            if (header)
            {
                yield return String.Join(separator, fields.Select(f => f.Name).Concat(properties.Select(p => p.Name)).ToArray());
            }
            foreach (var o in objectlist)
            {
                yield return string.Join(separator, fields.Select(f => (f.GetValue(o) ?? "").ToString().Trim()).Concat(properties.Select(p => (p.GetValue(o, null) ?? "").ToString())).ToArray());
            }
        }

        public async Task<object> GetFilteredData(List<ExpressionFilter> filters)
        {
            var response = await Host.ExchangeRateAPI.GetFilteredResult(filters);

            StringBuilder sb = new StringBuilder();

            foreach (var item in ToCsv(response, "|", true))
            {
                sb.AppendLine(item);
            }

            return sb.ToString();
        }

        public async Task<object> GetSortedData(List<ExpressionSort> sorts)
        {
            var response = await Host.ExchangeRateAPI.GetSortedResult(sorts);

            StringBuilder sb = new StringBuilder();

            foreach (var item in ToCsv(response, "|", true))
            {
                sb.AppendLine(item);
            }

            return sb.ToString();
        }

        public async Task<object> GetFilteredAndSortedData(List<ExpressionFilter> filters, List<ExpressionSort> sorts)
        {
            var response = await Host.ExchangeRateAPI.GetFilteredAndSortedResult(filters, sorts);

            StringBuilder sb = new StringBuilder();

            foreach (var item in ToCsv(response, "|", true))
            {
                sb.AppendLine(item);
            }

            return sb.ToString();
        }
    }
}
