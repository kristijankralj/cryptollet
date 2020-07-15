using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Cryptollet.Common.Base;
using Cryptollet.Common.Database;
using Cryptollet.Common.Dialog;
using Cryptollet.Common.Models;
using Cryptollet.Common.Navigation;
using Cryptollet.Common.Security;
using Cryptollet.Common.Validation;
using Cryptollet.Modules.Register;
using Cryptollet.Modules.Wallet;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Cryptollet.Modules.Login
{
    public class LoginViewModel: BaseViewModel
    {
        private INavigationService _navigationService;
        private IRepository<User> _userRepository;
        private IDialogMessage _dialogMessage;

        public LoginViewModel(Func<INavigationService> navigationService,
            IRepository<User> repository,
            IDialogMessage message)
        {
            _navigationService = navigationService();
            _userRepository = repository;
            _dialogMessage = message;
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
            if (!EntriesCorrectlyPopulated())
            {
                return;
            }
            IsBusy = true;
            var user = (await _userRepository.GetAllAsync())
                .FirstOrDefault(x => x.Email == Email.Value);
            if (user == null)
            {
                await DisplayCredentialsError();
                IsBusy = false;
                return;
            }
            if (!SecurePasswordHasher.Verify(Password.Value, user.HashedPassword))
            {
                await DisplayCredentialsError();
                IsBusy = false;
                return;
            }

            Preferences.Set(Constants.IS_USER_LOGGED_IN, true);
            Email.Value = string.Empty;
            Password.Value = string.Empty;
            _navigationService.GoToAppShell();
            IsBusy = false;
        }

        private async Task DisplayCredentialsError()
        {
            await _dialogMessage.DisplayAlert("Error", "Credentials are wrong.", "Ok");
            Password.Value = "";
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

        private bool EntriesCorrectlyPopulated()
        {
            _email.Validate();
            _password.Validate();

            return _email.IsValid && _password.IsValid;
        }
    }
}
