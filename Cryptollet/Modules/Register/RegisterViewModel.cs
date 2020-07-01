using System.Threading.Tasks;
using System.Windows.Input;
using Cryptollet.Common.Base;
using Cryptollet.Common.Database;
using Cryptollet.Common.Models;
using Cryptollet.Common.Navigation;
using Cryptollet.Common.Security;
using Cryptollet.Common.Validation;
using Cryptollet.Modules.Wallet;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Cryptollet.Modules.Register
{
    public class RegisterViewModel: BaseViewModel
    {
        private INavigationService _navigationService;
        private IRepository<User> _userRepository;

        public RegisterViewModel(INavigationService navigationService,
            IRepository<User> repository)
        {
            _navigationService = navigationService;
            _userRepository = repository;
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

        private ValidatableObject<string> _name;
        public ValidatableObject<string> Name
        {
            get => _name;
            set { SetProperty(ref _name, value); }
        }

        public ICommand LoginCommand { get => new Command(async () => await GoToLogin()); }
        public ICommand RegisterUserCommand { get => new Command(async () => await RegisterUser()); }

        private async Task RegisterUser()
        {
            IsBusy = true;
            if (!EntriesCorrectlyPopulated())
            {
                IsBusy = false;
                return;
            }
            var user = new User()
            {
                Email = Email.Value,
                FirstName = Name.Value,
                HashedPassword = SecurePasswordHasher.Hash(Password.Value)
            };
            await _userRepository.SaveAsync(user);

            Preferences.Set(Constants.IS_USER_LOGGED_IN, true);
            await _navigationService.InsertAsRoot<WalletViewModel>();
            IsBusy = false;
        }

        private async Task GoToLogin()
        {
            await _navigationService.PopAsync();
        }

        private void AddValidations()
        {
            _email = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();
            _name = new ValidatableObject<string>();

            _email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Email is empty." });
            _email.Validations.Add(new EmailRule<string> { ValidationMessage = "Email is not in correct format." });

            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password is empty." });

            _name.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Name is empty." });
        }

        private bool EntriesCorrectlyPopulated()
        {
            _email.Validate();
            _password.Validate();
            _name.Validate();

            return _email.IsValid && _password.IsValid && _name.IsValid;
        }
    }
}
