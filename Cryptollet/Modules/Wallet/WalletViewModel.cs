using System;
using System.Collections.ObjectModel;
using Cryptollet.Common.Base;
using Cryptollet.Common.Models;

namespace Cryptollet.Modules.Wallet
{
    public class WalletViewModel: BaseViewModel
    {

        public WalletViewModel()
        {
            Assets = new ObservableCollection<Coin>
            {
                new Coin { Name = "Bitcoin", Amount= 0.8934M, Symbol = "BTC", DollarValue = 8452.98M },
                new Coin { Name = "Ethereum", Amount= 8.0175M, Symbol = "ETH", DollarValue = 1825.72M },
                new Coin { Name = "Litecoin", Amount= 24.82M, Symbol = "LTC", DollarValue = 1378.45M },
            };
        }

        private ObservableCollection<Coin> _assets;
        public ObservableCollection<Coin> Assets
        {
            get => _assets;
            set { SetProperty(ref _assets, value); }
        }
    }
}
