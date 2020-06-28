using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Cryptollet.Common.Base;
using Cryptollet.Common.Navigation;
using Xamarin.Forms;

namespace Cryptollet.Modules.Register
{
    public class RegisterViewModel: BaseViewModel
    {
        private INavigationService _navigationService;

        public RegisterViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ICommand LoginCommand { get => new Command(async () => await GoToLogin()); }

        private async Task GoToLogin()
        {
            await _navigationService.PopAsync();
        }
    }
}
