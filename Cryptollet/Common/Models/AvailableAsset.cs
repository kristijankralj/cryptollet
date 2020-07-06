using System;
using System.Collections.Generic;

namespace Cryptollet.Common.Models
{
    public class AvailableAsset
    {
        public AvailableAsset(string coin, string imageUrl)
        {
            Coin = coin;
            ImageUrl = imageUrl;
        }

        public string Coin { get; set; }
        public string ImageUrl { get; set; }



        public static List<AvailableAsset> GetAvailableAssets()
        {
            return new List<AvailableAsset>
            {
                new AvailableAsset("Bitcoin", "BTC.png"),
                new AvailableAsset("Bitcoin Cash", "BCH.png"),
                new AvailableAsset("Dash", "DASH.png"),
                new AvailableAsset("Eos", "EOS.png"),
                new AvailableAsset("Ethereum", "ETH.png"),
                new AvailableAsset("Litecoin", "LTC.png"),
                new AvailableAsset("Monero", "MONERO.png"),
                new AvailableAsset("Ripple", "RIPPLE.png"),
                new AvailableAsset("Stellar", "STELLAR.png")
            };
        }
    }
}
