using System;
using System.Threading.Tasks;
using Cryptollet.Common.Base;
using Cryptollet.Common.Navigation;
using Cryptollet.Common.Settings;
using Cryptollet.Modules.Login;
using Xamarin.Essentials;

namespace Cryptollet.Modules.Loading
{
    public class LoadingViewModel: BaseViewModel
    {
        private INavigationService _navigationService;
        private IUserPreferences _userPreferences;

        public LoadingViewModel(INavigationService navigationService, IUserPreferences userPreferences)
        {
            _navigationService = navigationService;
            _userPreferences = userPreferences;
        }

        public override async Task InitializeAsync(object parameter)
        {
            await Task.Delay(500);
            if (!_userPreferences.ContainsKey(Constants.SHOWN_ONBOARDING))
            {
                _userPreferences.Set(Constants.SHOWN_ONBOARDING, true);
                _navigationService.GoToLoginFlow();
            }

            if (_userPreferences.ContainsKey(Constants.IS_USER_LOGGED_IN) && _userPreferences.Get(Constants.IS_USER_LOGGED_IN, false))
            {
                _navigationService.GoToMainFlow();
            }

            _navigationService.GoToLoginFlow();
            await _navigationService.InsertAsRoot<LoginViewModel>();
        }
    }
}
