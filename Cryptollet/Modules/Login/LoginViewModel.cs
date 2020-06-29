using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Cryptollet.Common.Base;
using Cryptollet.Common.Navigation;
using Cryptollet.Common.Validation;
using Cryptollet.Modules.Register;
using Cryptollet.Modules.Wallet;
using Xamarin.Forms;

namespace Cryptollet.Modules.Login
{
    public class LoginViewModel: BaseViewModel
    {
        private INavigationService _navigationService;

        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            AddValidations();
        }

        private ValidatableObject<string> _email;
        public ValidatableObject<string> Email
        {
            get => _email;
            set { SetProperty(ref _email, value); }
        }

        private ValidatableObject<string> _password;
        public ValidatableObject<string> Password
        {
            get => _password;
            set { SetProperty(ref _password, value); }
        }

        public ICommand RegisterCommand { get => new Command(async () => await GoToRegister()); }
        public ICommand LoginCommand { get => new Command(async () => await LoginUser(), () => IsNotBusy); }

        private async Task LoginUser()
        {
            IsBusy = true;
            if(AreEntriesCorrectlyPopulated())
            {
                await _navigationService.InsertAsRoot<WalletViewModel>();
            }
            IsBusy = false;
        }

        private async Task GoToRegister()
        {
            await _navigationService.PushAsync<RegisterViewModel>();
        }

        private void AddValidations()
        {
            _email = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();

            _email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Email is empty." });
            _email.Validations.Add(new EmailRule<string> { ValidationMessage = "Email is not in correct format." });

            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password is empty." });
        }

        private bool AreEntriesCorrectlyPopulated()
        {
            _email.Validate();
            _password.Validate();

            return _email.IsValid && _password.IsValid;
        }
    }
}
