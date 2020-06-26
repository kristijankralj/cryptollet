using System;
using System.Threading.Tasks;
using Cryptollet.Common.Navigation;
using Cryptollet.Modules.Login;

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
            await _navigationService.PushAsync<OnboardingViewModel, object>(onCompletion: FinishedOnboarding);
        }

        private async void FinishedOnboarding(object sender, object parameter)
        {
            await _navigationService.PushAsync<LoginViewModel>();
        }
    }
}
