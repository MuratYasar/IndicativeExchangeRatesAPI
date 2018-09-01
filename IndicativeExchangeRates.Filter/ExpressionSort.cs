using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndicativeExchangeRates.FilterSort
{
    public class ExpressionSort
    {
        /// <summary>
        /// You can display column name by using FilterColumNames enum
        /// </summary>
        [Required]
        public Enums.FilterColumNames PropertyName { get; set; }
        /// <summary>
        /// You can display appropriate options by using SortDirection enum
        /// </summary>
        [Required]
        public Enums.SortDirection SortDirection { get; set; }
    }
}
