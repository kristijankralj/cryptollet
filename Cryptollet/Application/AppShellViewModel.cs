using System.Threading.Tasks;
using System.Windows.Input;
using Cryptollet.Common.Base;
using Cryptollet.Common.Navigation;
using Cryptollet.Modules.Login;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Cryptollet
{
    public class AppShellViewModel: BaseViewModel
    {
        private INavigationService _navigationService;

        public AppShellViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ICommand SignOutCommand { get => new Command(async () => await SignOut()); }

        private async Task SignOut()
        {
            Preferences.Remove(Constants.IS_USER_LOGGED_IN);
            _navigationService.GoToLoginFlow();
            await _navigationService.InsertAsRoot<LoginViewModel>();
        }
    }
}
