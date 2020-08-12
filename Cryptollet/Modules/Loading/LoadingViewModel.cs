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

        public override Task InitializeAsync(object parameter)
        {
            if (!_userPreferences.ContainsKey(Constants.SHOWN_ONBOARDING))
            {
                _userPreferences.Set(Constants.SHOWN_ONBOARDING, true);
                _navigationService.GoToLoginFlow();
                return Task.CompletedTask;
            }

            if (_userPreferences.ContainsKey(Constants.IS_USER_LOGGED_IN) && _userPreferences.Get(Constants.IS_USER_LOGGED_IN, false))
            {
                _navigationService.GoToMainFlow();
                return Task.CompletedTask;
            }

            _navigationService.GoToLoginFlow();
            return _navigationService.InsertAsRoot<LoginViewModel>();
        }
    }
}
