using System; 

namespace ExchageRates.Models
{
    public class Exchange
    {
        public int Id { get; set; }
        public string ExchangeName{ get; set; }
        public DateTime DateExchange{ get; set; }
        public double ExchangeRates{ get; set; }
    }
}
