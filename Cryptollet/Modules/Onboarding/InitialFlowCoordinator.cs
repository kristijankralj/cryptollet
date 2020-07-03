using System;
using System.Threading.Tasks;
using Cryptollet;
using Cryptollet.Common.Navigation;
using Cryptollet.Modules.Login;
using Cryptollet.Modules.Wallet;
using Xamarin.Essentials;

namespace Cryptollet.Modules.Onboarding
{
    public class InitialFlowCoordinator
    {
        private INavigationService _navigationService;

        public InitialFlowCoordinator(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public async Task Start()
        {
            if (!Preferences.ContainsKey(Constants.SHOWN_ONBOARDING))
            {
                Preferences.Set(Constants.SHOWN_ONBOARDING, true);
                await _navigationService.PushAsync<OnboardingViewModel, object>(onCompletion: FinishedOnboarding);
                return;
            }

            if (Preferences.ContainsKey(Constants.IS_USER_LOGGED_IN) && Preferences.Get(Constants.IS_USER_LOGGED_IN, false))
            {
                await _navigationService.PushAsync<WalletViewModel>();
                return;
            }

            await _navigationService.PushAsync<LoginViewModel>();
        }

        private async void FinishedOnboarding(object sender, object parameter)
        {
            await _navigationService.PushAsync<LoginViewModel>();
        }
    }
}
