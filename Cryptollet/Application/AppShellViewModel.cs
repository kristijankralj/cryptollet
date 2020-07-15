using System.Windows.Input;
using Cryptollet.Common.Base;
using Cryptollet.Common.Navigation;
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

        public ICommand SignOutCommand { get => new Command(SignOut); }

        private void SignOut()
        {
            Preferences.Remove(Constants.IS_USER_LOGGED_IN);
            _navigationService.GoToStart();
        }
    }
}
