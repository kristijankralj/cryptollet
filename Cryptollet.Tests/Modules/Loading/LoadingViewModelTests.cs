using System;
using System.Threading.Tasks;
using Cryptollet.Common.Navigation;
using Cryptollet.Common.Settings;
using Cryptollet.Modules.Loading;
using Cryptollet.Modules.Login;
using Cryptollet.Tests.Mocks;
using Moq;
using Xunit;

namespace Cryptollet.Tests.Modules.Loading
{
    public class LoadingViewModelTests
    {
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IUserPreferences> _mockUserPreferences;

        public LoadingViewModelTests()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _mockUserPreferences = new Mock<IUserPreferences>();
        }

        [Fact]
        public async Task InitializeAsync_shows_onboarding_view_if_user_has_not_seen_it()
        {
            _mockUserPreferences.ContainsKeyReturns(Constants.SHOWN_ONBOARDING, false);
            LoadingViewModel viewModel = CreateLoadingViewModel();

            await viewModel.InitializeAsync(null);

            _mockNavigationService.VerifyThatGoToLoginFlowWasCalled();
            _mockNavigationService.VerifyThatInsertAsRootWasNotCalled<LoginViewModel>();
        }

        [Fact]
        public async Task InitializeAsync_shows_login_view_if_user_was_not_logged_in()
        {
            _mockUserPreferences.ContainsKeyReturns(Constants.SHOWN_ONBOARDING, true);
            _mockUserPreferences.ContainsKeyReturns(Constants.IS_USER_LOGGED_IN, false);
            _mockUserPreferences.GetReturns(Constants.IS_USER_LOGGED_IN, true);

            LoadingViewModel viewModel = CreateLoadingViewModel();

            await viewModel.InitializeAsync(null);

            _mockNavigationService.VerifyThatGoToLoginFlowWasCalled();
            _mockNavigationService.VerifyThatInsertAsRootWasCalled<LoginViewModel>();
        }

        [Fact]
        public async Task InitializeAsync_shows_main_view_when_the_user_is_logged_in()
        {
            _mockUserPreferences.ContainsKeyReturns(Constants.SHOWN_ONBOARDING, true);
            _mockUserPreferences.ContainsKeyReturns(Constants.IS_USER_LOGGED_IN, true);
            _mockUserPreferences.GetReturns(Constants.IS_USER_LOGGED_IN, true);

            LoadingViewModel viewModel = CreateLoadingViewModel();

            await viewModel.InitializeAsync(null);

            _mockNavigationService.VerifyThatGoToMainFlowWasCalled();
        }

        private LoadingViewModel CreateLoadingViewModel()
        {
            return new LoadingViewModel(_mockNavigationService.Object, _mockUserPreferences.Object);
        }
    }
}
