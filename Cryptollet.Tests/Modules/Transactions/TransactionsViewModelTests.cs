using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptollet.Common.Controllers;
using Cryptollet.Common.Models;
using Cryptollet.Common.Navigation;
using Cryptollet.Modules.AddTransaction;
using Cryptollet.Modules.Transactions;
using Cryptollet.Tests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cryptollet.Tests.Modules.Transactions
{
    public class TransactionsViewModelTests
    {
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IWalletController> _mockWalletController;

        private List<Transaction> _transactions = new List<Transaction>
        {
            new Transaction
            {
                 Amount = 1,
                 DollarValue = 11000,
                 Id = 1,
                 Status = Constants.TRANSACTION_DEPOSITED,
                 Symbol = "BTC",
            },
            new Transaction
            {
                 Amount = 0.1M,
                 DollarValue = 1100,
                 Id = 2,
                 Status = Constants.TRANSACTION_WITHDRAWN,
                 Symbol = "BTC"
            }
        };

        public TransactionsViewModelTests()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _mockWalletController = new Mock<IWalletController>();
        }

        [Fact]
        public async Task InitializeAsync_displays_all_transactions_on_all_transactions_view()
        {
            _mockWalletController.GetTransactionsReturns(_transactions);
            TransactionsViewModel viewModel = CreateTransactionsViewModel();

            await viewModel.InitializeAsync("");

            viewModel.Transactions.Should().HaveCount(2);
        }

        [Fact]
        public async Task InitializeAsync_displays_only_deposited_transactions_on_deposited_transactions_view()
        {
            _mockWalletController.GetTransactionsReturns(_transactions);
            TransactionsViewModel viewModel = CreateTransactionsViewModel();

            await viewModel.InitializeAsync(Constants.TRANSACTION_DEPOSITED);

            viewModel.Transactions.Should().HaveCount(1);
        }

        [Fact]
        public async Task InitializeAsync_displays_only_withdrawn_transactions_on_withdrawn_transactions_view()
        {
            _mockWalletController.GetTransactionsReturns(_transactions);
            TransactionsViewModel viewModel = CreateTransactionsViewModel();

            await viewModel.InitializeAsync(Constants.TRANSACTION_WITHDRAWN);

            viewModel.Transactions.Should().HaveCount(1);
        }

        [Fact]
        public void TradeCommand_navigates_to_the_add_transaction_view()
        {
            TransactionsViewModel viewModel = CreateTransactionsViewModel();

            viewModel.TradeCommand.Execute(null);

            _mockNavigationService.VerifyThatPushAsyncWasCalled<AddTransactionViewModel>();
        }

        private TransactionsViewModel CreateTransactionsViewModel()
        {
            return new TransactionsViewModel(_mockNavigationService.Object, _mockWalletController.Object);
        }
    }
}
