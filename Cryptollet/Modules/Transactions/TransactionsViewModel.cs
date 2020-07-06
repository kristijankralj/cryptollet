using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Cryptollet.Common.Base;
using Cryptollet.Common.Models;
using Cryptollet.Common.Navigation;
using Cryptollet.Modules.AddAsset;
using Xamarin.Forms;

namespace Cryptollet.Modules.Transactions
{
    public class TransactionsViewModel: BaseViewModel
    {
        private INavigationService _navigationService;

        public TransactionsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Transactions = new ObservableCollection<Transaction>
            {
                new Transaction
                {
                    Status = Constants.TRANSACTION_WITHDRAWN,
                    StatusImageSource = Constants.TRANSACTION_WITHDRAWN_IMAGE,
                    TransactionDate = new DateTime(2019, 8, 19),
                    Amount = 0.021M,
                    DollarValue = 204,
                    Symbol = "BTC"
                },
                new Transaction
                {
                    Status = Constants.TRANSACTION_DEPOSITED,
                    StatusImageSource = Constants.TRANSACTION_DEPOSITED_IMAGE,
                    TransactionDate = new DateTime(2019, 8, 16),
                    Amount = 3.21M,
                    DollarValue = 695.03M,
                    Symbol = "ETH"
                },
                new Transaction
                {
                    Status = Constants.TRANSACTION_DEPOSITED,
                    StatusImageSource = Constants.TRANSACTION_DEPOSITED_IMAGE,
                    TransactionDate = new DateTime(2019, 8, 10),
                    Amount = 37.81M,
                    DollarValue = 250M,
                    Symbol = "NEO"
                },
                new Transaction
                {
                    Status = Constants.TRANSACTION_WITHDRAWN,
                    StatusImageSource = Constants.TRANSACTION_WITHDRAWN_IMAGE,
                    TransactionDate = new DateTime(2019, 8, 5),
                    Amount = 0.021M,
                    DollarValue = 204,
                    Symbol = "BTC"
                },
                new Transaction
                {
                    Status = Constants.TRANSACTION_DEPOSITED,
                    StatusImageSource = Constants.TRANSACTION_DEPOSITED_IMAGE,
                    TransactionDate = new DateTime(2019, 8, 1),
                    Amount = 3.21M,
                    DollarValue = 695.03M,
                    Symbol = "ETH"
                },
            };
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
