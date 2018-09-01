using IndicativeExchangeRates.FilterSort;
using IndicativeExchangeRates.PluginContracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndicativeExchangeRates.Plugin.JSON
{
    public class OutputJSON : IPlugin
    {
        public string Name => this.GetType().Name;

        public string Description => "Produce JSON Output";

        //public async Task<object> GetAllData()
        //{
        //    var response = await Host.ExchangeRateAPI.GetResult();

        //    var output = JsonConvert.SerializeObject(response);

        //    return output;
        //}

        public async Task<object> GetAllData()
        {
            //var response = Task.Run<IQueryable<Model.Currency>>(() => Host.ExchangeRateAPI.GetResult());

            //var response = Host.ExchangeRateAPI.GetResult().Result;

            var t = Task.Run(() =>
            {
                return Host.ExchangeRateAPI.GetResult().GetAwaiter().GetResult();
            });

            //t.Wait();

            var output = JsonConvert.SerializeObject(t);

            return output;

            //var response = Task.Run<IQueryable<Model.Currency>>(() => Host.ExchangeRateAPI.GetResult());

            //var output = JsonConvert.SerializeObject(response);

            //return Task.FromResult((object)output);
        }

        public async Task<object> GetFilteredData(List<ExpressionFilter> filters)
        {
            var response = await Host.ExchangeRateAPI.GetFilteredResult(filters);

            var output = JsonConvert.SerializeObject(response);

            return output;
        }

        public async Task<object> GetSortedData(List<ExpressionSort> sorts)
        {
            var response = await Host.ExchangeRateAPI.GetSortedResult(sorts);

            var output = JsonConvert.SerializeObject(response);

            return output;
        }

        public async Task<object> GetFilteredAndSortedData(List<ExpressionFilter> filters, List<ExpressionSort> sorts)
        {
            var response = await Host.ExchangeRateAPI.GetFilteredAndSortedResult(filters, sorts);

            var output = JsonConvert.SerializeObject(response);

            return output;
        }
    }
}
