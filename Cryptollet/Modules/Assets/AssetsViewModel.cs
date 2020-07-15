using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Cryptollet.Common.Base;
using Cryptollet.Common.Controllers;
using Cryptollet.Common.Models;
using Cryptollet.Common.Navigation;
using Cryptollet.Modules.AddAsset;
using Xamarin.Forms;

namespace Cryptollet.Modules.Assets
{
    public class AssetsViewModel: BaseViewModel
    {
        private INavigationService _navigationService;
        private IWalletController _walletController;

        public AssetsViewModel(INavigationService navigationService,
                               IWalletController walletController)
        {
            _navigationService = navigationService;
            _walletController = walletController;
            Assets = new ObservableCollection<Coin>();
        }

        public override async Task InitializeAsync(object parameter)
        {
            var assets = await _walletController.GetCoins();
            Assets = new ObservableCollection<Coin>(assets);
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
