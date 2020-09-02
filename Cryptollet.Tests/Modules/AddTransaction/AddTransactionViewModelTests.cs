using System;
using System.Linq;
using System.Threading.Tasks;
using Cryptollet.Common.Database;
using Cryptollet.Common.Dialog;
using Cryptollet.Common.Models;
using Cryptollet.Common.Navigation;
using Cryptollet.Common.Settings;
using Cryptollet.Modules.AddTransaction;
using Cryptollet.Tests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cryptollet.Tests.Modules.AddTransaction
{
    public class AddTransactionViewModelTests
    {
        private Mock<IDialogMessage> _mockDialog;
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IRepository<Transaction>> _mockRepository;
        private Mock<IUserPreferences> _mockPreferences;

        public AddTransactionViewModelTests()
        {
            _mockDialog = new Mock<IDialogMessage>();
            _mockNavigationService = new Mock<INavigationService>();
            _mockRepository = new Mock<IRepository<Transaction>>();
            _mockPreferences = new Mock<IUserPreferences>();
        }

        [Fact]
        public void GoBack_doesnt_navigate_back_when_the_user_cancels_navigation()
        {
            _mockDialog.DisplayAlertReturns(false);
            AddTransactionViewModel viewModel = CreateAddTransactionViewModel();

            viewModel.BackCommand.Execute(null);

            _mockNavigationService.VerifyThatGoBackWasNotCalled();
        }

        [Fact]
        public void GoBack_navigates_back_when_the_user_confirms_navigation()
        {
            _mockDialog.DisplayAlertReturns(true);
            AddTransactionViewModel viewModel = CreateAddTransactionViewModel();

            viewModel.BackCommand.Execute(null);

            _mockNavigationService.VerifyThatGoBackWasCalledOnce();
        }

        [Fact]
        public void AddTransaction_validates_amount()
        {
            AddTransactionViewModel viewModel = CreateAddTransactionViewModel();

            viewModel.AddTransactionCommand.Execute(null);

            viewModel.Amount.Errors.First().Should().Be("Please enter amount greater than zero.");
        }

        [Fact]
        public void AddTransaction_displays_error_when_coin_is_not_selected()
        {
            AddTransactionViewModel viewModel = CreateAddTransactionViewModel();
            viewModel.Amount.Value = 1;

            viewModel.AddTransactionCommand.Execute(null);

            _mockDialog.VerifyThatDisplayAlertWasCalledWithMessage("Please select a coin.");
        }

        [Fact]
        public void AddTransaction_saves_new_transaction()
        {
            AddTransactionViewModel viewModel = CreateAddTransactionViewModel();
            viewModel.Amount.Value = 1;
            viewModel.SelectedCoin = Coin.GetAvailableAssets().First();

            viewModel.AddTransactionCommand.Execute(null);

            viewModel.Amount.Errors.Should().BeEmpty();

            _mockRepository.VerifyThatSaveAsyncWasCalled();
        }

        private AddTransactionViewModel CreateAddTransactionViewModel()
        {
            return new AddTransactionViewModel(_mockDialog.Object,
                                               _mockNavigationService.Object,
                                               _mockRepository.Object,
                                               _mockPreferences.Object);
        }
    }
}
