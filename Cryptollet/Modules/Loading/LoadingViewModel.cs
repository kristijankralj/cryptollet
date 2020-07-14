using System.Threading.Tasks;
using Cryptollet.Common.Base;
using Cryptollet.Common.Navigation;
using Cryptollet.Modules.Login;
using Cryptollet.Modules.Onboarding;
using Cryptollet.Modules.Wallet;
using Xamarin.Essentials;

namespace Cryptollet.Modules.Loading
{
    public class LoadingViewModel: BaseViewModel
    {
        private INavigationService _navigationService;

        public LoadingViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override Task InitializeAsync(object parameter)
        {
            if (!Preferences.ContainsKey(Constants.SHOWN_ONBOARDING))
            {
                Preferences.Set(Constants.SHOWN_ONBOARDING, true);
                return _navigationService.PushAsync<OnboardingViewModel>();
            }

            if (Preferences.ContainsKey(Constants.IS_USER_LOGGED_IN) && Preferences.Get(Constants.IS_USER_LOGGED_IN, false))
            {
                return _navigationService.PushAsync<WalletViewModel>();
            }

            return _navigationService.PushAsync<LoginViewModel>();
        }
    }
}
