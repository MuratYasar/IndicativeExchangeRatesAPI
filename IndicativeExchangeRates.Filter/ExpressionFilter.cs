using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndicativeExchangeRates.FilterSort
{
    public class ExpressionFilter
    {
        /// <summary>
        /// You can display column name by using FilterColumNames enum
        /// </summary>
        [Required]
        public Enums.FilterColumNames PropertyName { get; set; }
        /// <summary>
        /// You should specify the value for your criteria
        /// </summary>
        [Required]
        public object Value { get; set; }
        /// <summary>
        /// You can display appropriate options by using Comparison enum
        /// </summary>
        [Required]
        public Enums.Comparison Comparison { get; set; }
        /// <summary>
        /// You can display appropriate options by using LogicalOperation enum
        /// </summary>
        [Required]
        public Enums.LogicalOperation LogicalOperator { get; set; }
    }
}
