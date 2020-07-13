using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Cryptollet.Common.Base;
using Cryptollet.Common.Controllers;
using Cryptollet.Common.Models;
using Cryptollet.Common.Navigation;
using Cryptollet.Modules.AddAsset;
using Xamarin.Forms;

namespace Cryptollet.Modules.Transactions
{
    public class TransactionsViewModel: BaseViewModel
    {
        private INavigationService _navigationService;
        private IWalletController _walletController;

        public TransactionsViewModel(INavigationService navigationService,
                                     IWalletController walletController)
        {
            _navigationService = navigationService;
            _walletController = walletController;
            Transactions = new ObservableCollection<Transaction>();
        }

        public override async Task InitializeAsync(object parameter)
        {
            var transactions = await _walletController.GetTransactions();
            Transactions = new ObservableCollection<Transaction>(transactions);
        }

        private ObservableCollection<Transaction> _transactions;
        public ObservableCollection<Transaction> Transactions
        {
            get => _transactions;
            set { SetProperty(ref _transactions, value); }
        }

        public ICommand TradeCommand { get => new Command(async () => await Trade()); }

        private async Task Trade()
        {
            await _navigationService.PushAsync<AddAssetViewModel>();
        }
    }
}
