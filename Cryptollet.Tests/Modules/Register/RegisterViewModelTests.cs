using System;
using Cryptollet.Common.Database;
using Cryptollet.Common.Models;
using Cryptollet.Common.Navigation;
using Cryptollet.Common.Settings;
using Cryptollet.Modules.Login;
using Cryptollet.Modules.Register;
using Cryptollet.Tests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cryptollet.Tests.Modules.Register
{
    public class RegisterViewModelTests
    {
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IRepository<User>> _mockRepository;
        private Mock<IUserPreferences> _mockUserPreferences;

        public RegisterViewModelTests()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _mockRepository = new Mock<IRepository<User>>();
            _mockUserPreferences = new Mock<IUserPreferences>();
        }

        [Fact]
        public void LoginCommand_navigates_to_the_register_view()
        {
            RegisterViewModel viewModel = CreateRegisterViewModel();

            viewModel.LoginCommand.Execute(null);

            _mockNavigationService.VerifyThatInsertAsRootWasCalled<LoginViewModel>();
        }

        [Fact]
        public void RegisterUserCommand_validates_name_and_email_and_password()
        {
            RegisterViewModel viewModel = CreateRegisterViewModel();

            viewModel.RegisterUserCommand.Execute(null);

            viewModel.Name.Errors.Should().NotBeEmpty();
            viewModel.Email.Errors.Should().NotBeEmpty();
            viewModel.Password.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public void RegisterUserCommand_registers_new_user()
        {
            RegisterNewUser();

            _mockRepository.VerifyThatSaveAsyncWasCalled();
        }

        [Fact]
        public void RegisterUserCommand_navigates_to_the_main_flow()
        {
            RegisterNewUser();

            _mockNavigationService.VerifyThatGoToMainFlowWasCalled();
        }

        private void RegisterNewUser()
        {
            RegisterViewModel viewModel = CreateRegisterViewModel();
            viewModel.Name.Value = "Tester";
            viewModel.Email.Value = "email@crypto.com";
            viewModel.Password.Value = "pass";

            viewModel.RegisterUserCommand.Execute(null);
        }

        private RegisterViewModel CreateRegisterViewModel()
        {
            return new RegisterViewModel(_mockNavigationService.Object,
                                        _mockRepository.Object, _mockUserPreferences.Object);
        }
    }
}
