using System;
using System.Collections.Generic;
using Cryptollet.Common.Database;
using Cryptollet.Common.Dialog;
using Cryptollet.Common.Models;
using Cryptollet.Common.Navigation;
using Cryptollet.Common.Security;
using Cryptollet.Common.Settings;
using Cryptollet.Modules.Login;
using Cryptollet.Modules.Register;
using Cryptollet.Tests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cryptollet.Tests.Modules.Login
{
    public class LoginViewModelTests
    {
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IRepository<User>> _mockRepository;
        private Mock<IUserPreferences> _mockUserPreferences;
        private Mock<IDialogMessage> _mockDialogMessage;


        public LoginViewModelTests()
        {
            _mockDialogMessage = new Mock<IDialogMessage>();
            _mockNavigationService = new Mock<INavigationService>();
            _mockRepository = new Mock<IRepository<User>>();
            _mockUserPreferences = new Mock<IUserPreferences>();
        }

        [Fact]
        public void RegisterCommand_navigates_to_the_register_view()
        {
            LoginViewModel viewModel = CreateLoginViewModel();

            viewModel.RegisterCommand.Execute(null);

            _mockNavigationService.VerifyThatInsertAsRootWasCalled<RegisterViewModel>();
        }

        [Fact]
        public void LoginCommand_validates_email_and_password()
        {
            LoginViewModel viewModel = CreateLoginViewModel();

            viewModel.LoginCommand.Execute(null);

            viewModel.Email.Errors.Should().NotBeEmpty();
            viewModel.Password.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public void LoginCommand_shows_error_when_email_is_not_correct()
        {
            _mockRepository.GetAllAsyncReturns(new List<User>());
            LoginViewModel viewModel = CreateLoginViewModel();
            viewModel.Email.Value = "email@crypto.com";
            viewModel.Password.Value = "pass";

            viewModel.LoginCommand.Execute(null);

            _mockDialogMessage.VerifyThatDisplayAlertWasCalledWithMessage("Credentials are wrong.");
        }

        [Fact]
        public void LoginCommand_navigates_to_the_main_flow_when_email_and_password_are_correct()
        {
            _mockRepository.GetAllAsyncReturns(new List<User>()
            {
                new User
                {
                     Email = "email@crypto.com",
                     HashedPassword = SecurePasswordHasher.Hash("pass")
                }
            });
            LoginViewModel viewModel = CreateLoginViewModel();
            viewModel.Email.Value = "email@crypto.com";
            viewModel.Password.Value = "pass";

            viewModel.LoginCommand.Execute(null);

            _mockNavigationService.VerifyThatGoToMainFlowWasCalled();
        }

        private LoginViewModel CreateLoginViewModel()
        {
            return new LoginViewModel(_mockNavigationService.Object,
                _mockRepository.Object, _mockDialogMessage.Object, _mockUserPreferences.Object);
        }
    }
}
