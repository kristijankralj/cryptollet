using System;
namespace Cryptollet.Common.Models
{
    public class Coin
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal DollarValue { get; set; }
        public decimal Amount { get; set; }
        public decimal Change { get; set; }
    }
}
