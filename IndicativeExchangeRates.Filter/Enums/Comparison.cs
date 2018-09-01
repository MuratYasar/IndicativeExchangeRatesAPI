using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndicativeExchangeRates.FilterSort.Enums
{
    public enum Comparison
    {
        Equal,
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual,
        NotEqual,
        /// <summary>
        /// For string value
        /// </summary>
        Contains,
        /// <summary>
        /// For string value
        /// </summary>
        StartsWith,
        /// <summary>
        /// For string value
        /// </summary>
        EndsWith
    }
}
