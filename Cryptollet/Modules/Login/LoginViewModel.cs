using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Cryptollet.Common.Base;
using Cryptollet.Common.Navigation;
using Cryptollet.Modules.Register;
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

        private async Task GoToRegister()
        {
            await _navigationService.PushAsync<RegisterViewModel>();
        }
    }
}
