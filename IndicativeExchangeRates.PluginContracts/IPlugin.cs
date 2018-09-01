using IndicativeExchangeRates.FilterSort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndicativeExchangeRates.PluginContracts
{
    public interface IPlugin
    {
        string Name { get; }
        string Description { get; }
        Task<object> GetAllData();
        Task<object> GetFilteredData(List<ExpressionFilter> filters);
        Task<object> GetSortedData(List<ExpressionSort> sorts);
        Task<object> GetFilteredAndSortedData(List<ExpressionFilter> filters, List<ExpressionSort> sorts);
    }
}
