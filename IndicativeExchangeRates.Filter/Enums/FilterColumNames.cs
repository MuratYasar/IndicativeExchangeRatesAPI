using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndicativeExchangeRates.FilterSort.Enums
{
    public enum FilterColumNames
    {
        CrossOrder,
        /// <summary>
        /// Kod values might be one of the followings
        /// <para>USD (US DOLLAR)</para>
        /// <para>AUD (AUSTRALIAN DOLLAR)</para>
        /// <para>DKK (DANISH KRONE)</para>
        /// <para>EUR (EURO)</para>
        /// <para>GBP (POUND STERLING)</para>
        /// <para>CHF (SWISS FRANK)</para>
        /// <para>SEK (SWEDISH KRONA)</para>
        /// <para>CAD (CANADIAN DOLLAR)</para>
        /// <para>KWD (KUWAITI DINAR)</para>
        /// <para>NOK (NORWEGIAN KRONE)</para>
        /// <para>SAR (SAUDI RIYAL)</para>
        /// <para>JPY (JAPENESE YEN)</para>
        /// <para>BGN (BULGARIAN LEV)</para>
        /// <para>RON (NEW LEU)</para>
        /// <para>RUB (RUSSIAN ROUBLE)</para>
        /// <para>IRR (IRANIAN RIAL)</para>
        /// <para>CNY (CHINESE RENMINBI)</para>
        /// <para>PKR (PAKISTANI RUPEE)</para>
        /// <para>XDR (SPECIAL DRAWING RIGHT (SDR))</para>
        /// </summary>
        Kod,
        /// <summary>
        /// CurrencyCode values might be one of the followings
        /// <para>USD (US DOLLAR)</para>
        /// <para>AUD (AUSTRALIAN DOLLAR)</para>
        /// <para>DKK (DANISH KRONE)</para>
        /// <para>EUR (EURO)</para>
        /// <para>GBP (POUND STERLING)</para>
        /// <para>CHF (SWISS FRANK)</para>
        /// <para>SEK (SWEDISH KRONA)</para>
        /// <para>CAD (CANADIAN DOLLAR)</para>
        /// <para>KWD (KUWAITI DINAR)</para>
        /// <para>NOK (NORWEGIAN KRONE)</para>
        /// <para>SAR (SAUDI RIYAL)</para>
        /// <para>JPY (JAPENESE YEN)</para>
        /// <para>BGN (BULGARIAN LEV)</para>
        /// <para>RON (NEW LEU)</para>
        /// <para>RUB (RUSSIAN ROUBLE)</para>
        /// <para>IRR (IRANIAN RIAL)</para>
        /// <para>CNY (CHINESE RENMINBI)</para>
        /// <para>PKR (PAKISTANI RUPEE)</para>
        /// <para>XDR (SPECIAL DRAWING RIGHT (SDR))</para>
        /// </summary>
        CurrencyCode,
        Unit,
        CurrencyName,
        ForexBuying,
        ForexSelling,
        BanknoteBuying,
        BanknoteSelling,
        CrossRateUSD,
        CrossRateOther
    }
}
