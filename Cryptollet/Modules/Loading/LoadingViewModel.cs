using System;
using System.Threading.Tasks;
using Cryptollet.Common.Base;
using Cryptollet.Common.Navigation;
using Cryptollet.Modules.Login;
using Cryptollet.Modules.Onboarding;
using Xamarin.Essentials;

namespace Cryptollet.Modules.Loading
{
    public class LoadingViewModel: BaseViewModel
    {
        private INavigationService _navigationService;

        public LoadingViewModel(Func<INavigationService> navigationService)
        {
            _navigationService = navigationService();
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
                _navigationService.GoToAppShell();
                return Task.CompletedTask;
            }

            return _navigationService.PushAsync<LoginViewModel>();
        }
    }
}
