using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cryptollet.Common.Models
{
    public class Coin
    {
        public Coin(string name, string symbol, string imageUrl)
        {
            Name = name;
            Symbol = symbol;
            ImageUrl = imageUrl;
        }
        
        public Coin(){}

        [JsonProperty("id")]
        public int CoinId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
        public decimal Amount { get; set; }
        public decimal DollarValue { get; set; }
        public string ImageUrl { get; set; }

        public static List<Coin> GetAvailableAssets()
        {
            return new List<Coin>
            {
                new Coin("Bitcoin", "BTC", "BTC.png"),
                new Coin("Bitcoin Cash", "BCH", "BCH.png"),
                new Coin("Dash", "DASH", "DASH.png"),
                new Coin("Eos", "EOS", "EOS.png"),
                new Coin("Ethereum", "ETH", "ETH.png"),
                new Coin("Litecoin", "LTC", "LTC.png"),
                new Coin("Monero", "XMR", "MONERO.png"),
                new Coin("Ripple", "XRP", "RIPPLE.png"),
                new Coin("Stellar", "XLM", "STELLAR.png")
            };
        }
    }
}
