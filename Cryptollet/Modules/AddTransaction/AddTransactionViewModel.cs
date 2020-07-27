using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Cryptollet.Common.Base;
using Cryptollet.Common.Database;
using Cryptollet.Common.Dialog;
using Cryptollet.Common.Models;
using Cryptollet.Common.Navigation;
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

        public AddTransactionViewModel(IDialogMessage dialogMessage,
                                 INavigationService navigationService,
                                 IRepository<Transaction> transactionRepository)
        {
            _dialogMessage = dialogMessage;
            _transactionRepository = transactionRepository;
            _navigationService = navigationService;
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

        public ICommand AddAssetCommand { get => new Command(async () => await AddAsset(),() => IsNotBusy); }

        private async Task AddAsset()
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
            var transaction = new Transaction
            {
                Amount = Amount.Value,
                TransactionDate = TransactionDate,
                Symbol = SelectedCoin.Symbol,
                Status = IsDeposit ? Constants.TRANSACTION_DEPOSITED : Constants.TRANSACTION_WITHDRAWN,
                Id = string.IsNullOrEmpty(Id) ? 0 : int.Parse(Id)  
            };
            await _transactionRepository.SaveAsync(transaction);
        }

        private void AddValidations()
        {
            _amount = new ValidatableObject<decimal>();
            _amount.Validations.Add(new NonNegativeRule { ValidationMessage = "Please enter amount greated than zero." });
        }

        private bool EntriesAreCorrectlyPopulated()
        {
            _amount.Validate();
            return _amount.IsValid;
        }
    }
}
