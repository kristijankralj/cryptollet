using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Cryptollet.Common.Base;
using Cryptollet.Common.Models;
using Cryptollet.Common.Navigation;
using Cryptollet.Modules.AddAsset;
using Xamarin.Forms;

namespace Cryptollet.Modules.Assets
{
    public class AssetsViewModel: BaseViewModel
    {
        private INavigationService _navigationService;

        public AssetsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Assets = new ObservableCollection<Coin>
            {
                new Coin { Name = "Bitcoin", Amount= 0.8934M, Symbol = "BTC", DollarValue = 8452.98M, Change= 5.24M },
                new Coin { Name = "Ethereum", Amount= 8.0175M, Symbol = "ETH", DollarValue = 1825.72M, Change = 1.45M },
                new Coin { Name = "Litecoin", Amount= 24.82M, Symbol = "LTC", DollarValue = 1378.45M, Change = -0.91M },
            };
        }

        private ObservableCollection<Coin> _assets;
        public ObservableCollection<Coin> Assets
        {
            get => _assets;
            set { SetProperty(ref _assets, value); }
        }

        public ICommand AddAssetCommand { get => new Command(async () => await AddAsset()); }
        private async Task AddAsset()
        {
            await _navigationService.PushAsync<AddAssetViewModel>();
        }
    }
}
