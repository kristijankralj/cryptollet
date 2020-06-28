using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Cryptollet.Common.Base;
using Cryptollet.Common.Navigation;
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
        }

        public ICommand RegisterCommand { get => new Command(async () => await GoToRegister()); }
        public ICommand LoginCommand { get => new Command(async () => await LoginUser(), () => IsNotBusy); }

        private async Task LoginUser()
        {
            IsBusy = true;
            await _navigationService.InsertAsRoot<WalletViewModel>();
        }

        private async Task GoToRegister()
        {
            await _navigationService.PushAsync<RegisterViewModel>();
        }
    }
}
