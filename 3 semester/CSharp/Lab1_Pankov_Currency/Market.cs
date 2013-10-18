using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pankov.Lab1.CurrencyLab
{
    public static class Market
    {
        internal static Dictionary<Symbols, decimal> BuyRates = new Dictionary<Symbols, decimal>();
        internal static Dictionary<Symbols, decimal> SellRates = new Dictionary<Symbols, decimal>();

        static Market()
        {
            BuyRates[Symbols.USD] = 1;
            SellRates[Symbols.USD] = 1;
            BuyRates[Symbols.GBP] = 1.57852M;
            SellRates[Symbols.GBP] =1/ 0.633503M;
            BuyRates[Symbols.CAD] = 1.0146M;
            SellRates[Symbols.CAD] = 1/0.98561M;
            BuyRates[Symbols.EUR] = 1.376M;
            SellRates[Symbols.EUR] = 1/0.726744M;
            BuyRates[Symbols.AUD] = 1.03411M;
            SellRates[Symbols.AUD] =1/ 0.967006M;
        }

        /// <summary>
        /// Gets the conversion ratio between currencies
        /// </summary>
        /// <param name="from">Source currency</param>
        /// <param name="to">Destination currency</param>
        /// <returns>Conversion ratio</returns>
        public static decimal GetConversionRatio(Symbols from, Symbols to)
        {
            return SellRates[from] / BuyRates[to];
        }
    }
}
