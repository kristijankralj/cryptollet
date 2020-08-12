using System;
using Cryptollet.Common.Navigation;
using Cryptollet.Modules.Login;
using Cryptollet.Modules.Onboarding;
using Cryptollet.Tests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cryptollet.Tests.Modules.Onboarding
{
    public class OnboardingViewModelTests
    {
        private Mock<INavigationService> _mockNavigationService;

        public OnboardingViewModelTests()
        {
            _mockNavigationService = new Mock<INavigationService>();
        }

        [Fact]
        public void OnboardingSteps_contains_items()
        {
            OnboardingViewModel viewModel = CreateOnboardingViewModel();

            viewModel.OnboardingSteps.Should().NotBeEmpty();
        }

        [Fact]
        public void SkipCommand_navigates_to_the_login_view()
        {
            OnboardingViewModel viewModel = CreateOnboardingViewModel();

            viewModel.SkipCommand.Execute(null);

            _mockNavigationService.VerifyThatInsertAsRootWasCalled<LoginViewModel>();
        }

        private OnboardingViewModel CreateOnboardingViewModel()
        {
            return new OnboardingViewModel(_mockNavigationService.Object);
        }
    }
}
