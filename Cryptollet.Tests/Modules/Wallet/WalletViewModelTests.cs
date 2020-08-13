using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptollet.Common.Controllers;
using Cryptollet.Common.Models;
using Cryptollet.Common.Navigation;
using Cryptollet.Modules.AddTransaction;
using Cryptollet.Modules.Wallet;
using Cryptollet.Tests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cryptollet.Tests.Modules.Wallet
{
    public class WalletViewModelTests
    {
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IWalletController> _mockWalletController;

        private List<Coin> _defaultAssets = new List<Coin>
        {
                new Coin
                {
                    Name = "Bitcoin",
                    Amount = 1M,
                    Symbol = "BTC",
                    DollarValue = 11000
                },
                new Coin
                {
                    Name = "Ethereum",
                    Amount = 1,
                    Symbol = "ETH",
                    DollarValue = 400
                },
                new Coin
                {
                    Name = "Litecoin",
                    Amount = 1,
                    Symbol = "LTC",
                    DollarValue = 200
                },
        };

        private List<Transaction> _transactions = new List<Transaction>
        {
            new Transaction
            {
                 Amount = 1,
                 DollarValue = 11000,
                 Id = 1,
                 Status = Constants.TRANSACTION_DEPOSITED,
                 Symbol = "BTC"
            }
        };

        public WalletViewModelTests()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _mockWalletController = new Mock<IWalletController>();
        }

        [Fact]
        public void AddNewTransactionCommand_navigates_to_the_new_transaction_view()
        {
            var viewModel = CreateWalletViewModel();

            viewModel.AddNewTransactionCommand.Execute(null);

            _mockNavigationService.VerifyThatPushAsyncWasCalled<AddTransactionViewModel>();
        }

        [Fact]
        public async Task InitializeAsync_builds_the_chart()
        {
            _mockWalletController.GetCoinsReturns(_defaultAssets);
            _mockWalletController.GetTransactionsReturns(_transactions);
            var viewModel = CreateWalletViewModel();

            await viewModel.InitializeAsync(false);

            viewModel.PortfolioView.Should().NotBeNull();
        }

        [Fact]
        public async Task InitializeAsync_populates_the_latest_transactions_and_assets()
        {
            _mockWalletController.GetCoinsReturns(_defaultAssets);
            _mockWalletController.GetTransactionsReturns(_transactions);
            var viewModel = CreateWalletViewModel();

            await viewModel.InitializeAsync(false);

            viewModel.LatestTransactions.Should().NotBeEmpty();
            viewModel.Assets.Should().NotBeEmpty();
        }

        [Fact]
        public async Task InitializeAsync_calculates_the_portfolio_value()
        {
            _mockWalletController.GetCoinsReturns(_defaultAssets);
            _mockWalletController.GetTransactionsReturns(_transactions);
            var viewModel = CreateWalletViewModel();

            await viewModel.InitializeAsync(false);

            viewModel.PortfolioValue.Should().Be(11600);
        }

        private WalletViewModel CreateWalletViewModel()
        {
            return new WalletViewModel(_mockNavigationService.Object, _mockWalletController.Object);
        }
    }
}
