using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Cryptollet.Common.Base;
using Cryptollet.Common.Controllers;
using Cryptollet.Common.Models;
using Cryptollet.Common.Navigation;
using Cryptollet.Modules.AddTransaction;
using Xamarin.Forms;

namespace Cryptollet.Modules.Transactions
{
    public class WithdrawnTransactionsViewModel : BaseViewModel
    {
        private INavigationService _navigationService;
        private IWalletController _walletController;

        public WithdrawnTransactionsViewModel(INavigationService navigationService,
                                     IWalletController walletController)
        {
            _navigationService = navigationService;
            _walletController = walletController;
            Transactions = new ObservableCollection<Transaction>();
        }

        public override async Task InitializeAsync(object parameter)
        {
            IsRefreshing = true;
            var transactions = await _walletController.GetTransactions();
            transactions = transactions.Where(x => x.Status == Constants.TRANSACTION_WITHDRAWN).ToList();
            Transactions = new ObservableCollection<Transaction>(transactions);
            IsRefreshing = false;
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set { SetProperty(ref _isRefreshing, value); }
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
            await _navigationService.PushAsync<AddTransactionViewModel>();
        }
    }
}
