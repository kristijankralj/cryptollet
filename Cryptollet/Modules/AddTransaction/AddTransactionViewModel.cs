using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Cryptollet.Common.Base;
using Cryptollet.Common.Database;
using Cryptollet.Common.Dialog;
using Cryptollet.Common.Models;
using Cryptollet.Common.Navigation;
using Cryptollet.Common.Settings;
using Cryptollet.Common.Validation;
using Xamarin.Forms;

namespace Cryptollet.Modules.AddTransaction
{
    [QueryProperty("Id", "id")]
    public class AddTransactionViewModel: BaseViewModel
    {
        private readonly IDialogMessage _dialogMessage;
        private readonly INavigationService _navigationService;
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IUserPreferences _userPreferences;

        public AddTransactionViewModel(IDialogMessage dialogMessage,
                                 INavigationService navigationService,
                                 IRepository<Transaction> transactionRepository,
                                 IUserPreferences userPreferences)
        {
            _dialogMessage = dialogMessage;
            _transactionRepository = transactionRepository;
            _navigationService = navigationService;
            _userPreferences = userPreferences;
            AvailableAssets = new ObservableCollection<Coin>(Coin.GetAvailableAssets());
            AddValidations();
        }

        public override async Task InitializeAsync(object parameter)
        {
            if (string.IsNullOrEmpty(Id) || !int.TryParse(Id, out int transactionId))
            {
                IsDeposit = true;
                TransactionDate = DateTime.Now;
                return;
            }
            var selectedTransaction = await _transactionRepository.GetById(transactionId);
            IsDeposit = selectedTransaction.Status == Constants.TRANSACTION_DEPOSITED;
            Amount.Value = selectedTransaction.Amount;
            TransactionDate = selectedTransaction.TransactionDate;
            SelectedCoin = Coin.GetAvailableAssets().First(x => x.Symbol == selectedTransaction.Symbol);
        }

        private ObservableCollection<Coin> _availableAssets;
        public ObservableCollection<Coin> AvailableAssets
        {
            get => _availableAssets;
            set { SetProperty(ref _availableAssets, value); }
        }

        private Coin _selectedCoin;
        public Coin SelectedCoin
        {
            get => _selectedCoin;   
            set { SetProperty(ref _selectedCoin, value); }
        }

        private ValidatableObject<decimal> _amount;
        public ValidatableObject<decimal> Amount
        {
            get => _amount;
            set { SetProperty(ref _amount, value); }
        }
        private DateTime _transactionDate;
        public DateTime TransactionDate
        {
            get => _transactionDate;
            set { SetProperty(ref _transactionDate, value); }
        }

        private bool _isDeposit;
        public bool IsDeposit
        {
            get => _isDeposit;
            set { SetProperty(ref _isDeposit, value); }
        }

        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                _id = Uri.UnescapeDataString(value);
            }
        }

        public ICommand AddTransactionCommand { get => new Command(async () => await AddTransaction(),() => IsNotBusy); }
        public ICommand BackCommand { get => new Command(async () => await GoBack()); }

        private async Task GoBack()
        {
            var shouldGoBack = await _dialogMessage.DisplayAlert("Confirm",
                "Are you sure you want to navigate back? Any unsaved changes will be lost.", "Ok", "Canel");
            if (shouldGoBack)
            {
                await _navigationService.GoBackAsync();
            }
        }


        private async Task AddTransaction()
        {
            if (!EntriesAreCorrectlyPopulated())
            {
                return;
            }
            if (SelectedCoin == null)
            {
                await _dialogMessage.DisplayAlert("Error", "Please select a coin.", "Ok");
                return;
            }
            IsBusy = true;
            await SaveNewTransaction();
            await _navigationService.PopAsync();
            IsBusy = false;
        }

        private async Task SaveNewTransaction()
        {
            var userId = _userPreferences.Get(Constants.USER_ID, string.Empty);
            var transaction = new Transaction
            {
                Amount = Amount.Value,
                TransactionDate = TransactionDate,
                Symbol = SelectedCoin.Symbol,
                Status = IsDeposit ? Constants.TRANSACTION_DEPOSITED : Constants.TRANSACTION_WITHDRAWN,
                Id = string.IsNullOrEmpty(Id) ? 0 : int.Parse(Id),
                UserEmail = userId
            };
            await _transactionRepository.SaveAsync(transaction);
        }

        private void AddValidations()
        {
            _amount = new ValidatableObject<decimal>();
            _amount.Validations.Add(new NonNegativeRule { ValidationMessage = "Please enter amount greater than zero." });
        }

        private bool EntriesAreCorrectlyPopulated()
        {
            _amount.Validate();
            return _amount.IsValid;
        }
    }
}
