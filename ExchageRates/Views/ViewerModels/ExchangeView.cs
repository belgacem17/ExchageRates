using ExchageRates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchageRates.Views.ViewerModels
{
    public class ExchangeView
    {
         public List<Exchange> Exchanges = new List<Exchange>();
         public double MoyExchangeGBP { get; set; }
         public double MoyExchangeEUR { get; set; }
         public double MoyExchangeKursGBP { get; set; }
         public double MoyExchangeKursEUR { get; set; }
         public double MoyExchangeKursUSD { get; set; }
    }
}
