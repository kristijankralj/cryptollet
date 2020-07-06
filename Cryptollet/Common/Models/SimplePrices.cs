using System;
namespace Cryptollet.Common.Models
{
    public class Litecoin
    {
        public double Usd { get; set; }

    }

    public class Ethereum
    {
        public double Usd { get; set; }

    }

    public class Bitcoin
    {
        public double Usd { get; set; }

    }

    public class SimplePricesResult
    {
        public Litecoin Litecoin { get; set; }
        public Ethereum Ethereum { get; set; }
        public Bitcoin Bitcoin { get; set; }
    }
}
