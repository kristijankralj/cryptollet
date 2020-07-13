using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Cryptollet.Common.Base;
using Cryptollet.Common.Database;
using Cryptollet.Common.Models;
using Cryptollet.Common.Navigation;
using Cryptollet.Common.Network;
using Cryptollet.Modules.AddAsset;
using Cryptollet.Modules.Assets;
using Cryptollet.Modules.Login;
using Cryptollet.Modules.Transactions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Cryptollet.Modules.Wallet
{
    public class WalletViewModel: BaseViewModel
    {
        private INavigationService _navigationService;
        private IRepository<Transaction> _transactionRepository;
        private ICrypoService _crypoService;
        private List<Coin> _coins = new List<Coin>();

        public WalletViewModel(INavigationService navigationService,
                               ICrypoService crypoService,
                               IRepository<Transaction> transactionRepository)
        {
            _navigationService = navigationService;
            _crypoService = crypoService;
            _transactionRepository = transactionRepository;
            Assets = new ObservableCollection<Coin>();
            LatestTransactions = new ObservableCollection<Transaction>();
        }

        public override async Task InitializeAsync(object parameter)
        {
            await LoadAssets();
            await LoadTransactions();
        }

        private async Task LoadTransactions()
        {
            var transactions = await _transactionRepository.GetAllAsync();
            if (transactions.Count == 0 || _coins.Count == 0)
            {
                return;
            }
            transactions.ForEach(t =>
            {
                t.StatusImageSource = t.Status == Constants.TRANSACTION_DEPOSITED ?
                                        Constants.TRANSACTION_DEPOSITED_IMAGE :
                                        Constants.TRANSACTION_WITHDRAWN_IMAGE;
                t.DollarValue = t.Amount * (decimal)_coins.First(x => x.Symbol == t.Symbol).Price;
            });
            LatestTransactions = new ObservableCollection<Transaction>(transactions);
        }

        private async Task LoadAssets()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;
            IsRefreshing = true;
            _coins = await _crypoService.GetLatestPrices();
            Assets = new ObservableCollection<Coin>()
            {
                new Coin
                {
                    Name = "Bitcoin",
                    Amount = 1M,
                    Symbol = "BTC",
                    DollarValue = 1M * (decimal)_coins.FirstOrDefault(x => x.Symbol == "BTC").Price
                },
                new Coin
                {
                    Name = "Ethereum",
                    Amount = 8.0175M,
                    Symbol = "ETH",
                    DollarValue = 8.0175M * (decimal)_coins.FirstOrDefault(x => x.Symbol == "ETH").Price
                },
                new Coin
                {
                    Name = "Litecoin",
                    Amount = 24.82M,
                    Symbol = "LTC",
                    DollarValue = 24.82M * (decimal)_coins.FirstOrDefault(x => x.Symbol == "LTC").Price
                },
            };
            IsRefreshing = false;
            IsBusy = false;
        }

        private ObservableCollection<Coin> _assets;
        public ObservableCollection<Coin> Assets
        {
            get => _assets;
            set { SetProperty(ref _assets, value); }
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set { SetProperty(ref _isRefreshing, value); }
        }

        private ObservableCollection<Transaction> _latestTransactions;
        public ObservableCollection<Transaction> LatestTransactions
        {
            get => _latestTransactions;
            set
            {
                SetProperty(ref _latestTransactions, value);
                if (_latestTransactions == null)
                {
                    return;
                }
                HasTransactions = _latestTransactions.Count > 0;
                if (_latestTransactions.Count == 0)
                {
                    TransactionsHeight = 200;
                }
                TransactionsHeight = _latestTransactions.Count * 90;
            }
        }

        private bool _hasTransactions;
        public bool HasTransactions
        {
            get => _hasTransactions;
            set { SetProperty(ref _hasTransactions, value); }
        }

        private int _transactionsHeight;
        public int TransactionsHeight
        {
            get => _transactionsHeight;
            set { SetProperty(ref _transactionsHeight, value); }
        }

        public ICommand GoToAssetsCommand { get => new Command(async () => await GoToAssets()); }
        public ICommand GoToTransactionsCommand { get => new Command(async () => await GoToTransactions()); }
        public ICommand SignOutCommand { get => new Command(async () => await SignOut()); }
        public ICommand RefreshAssetsCommand { get => new Command(async () => await LoadAssets()); }
        public ICommand AddNewTransactionCommand { get => new Command(async () => await  AddNewTransaction()); }

        private async Task  AddNewTransaction()
        {
            await _navigationService.PushAsync<AddAssetViewModel>();
        }

        private async Task SignOut()
        {
            Preferences.Remove(Constants.IS_USER_LOGGED_IN);
            await _navigationService.InsertAsRoot<LoginViewModel>();
        }

        private async Task GoToTransactions()
        {
            await _navigationService.PushAsync<TransactionsViewModel>();
        }

        private async Task GoToAssets()
        {
            await _navigationService.PushAsync<AssetsViewModel>();
        }
    }
}
