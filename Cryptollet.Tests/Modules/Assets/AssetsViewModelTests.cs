using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptollet.Common.Controllers;
using Cryptollet.Common.Models;
using Cryptollet.Common.Navigation;
using Cryptollet.Modules.AddTransaction;
using Cryptollet.Modules.Assets;
using Cryptollet.Tests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cryptollet.Tests.Modules.Assets
{
    public class AssetsViewModelTests
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
                    Amount = 0,
                    Symbol = "ETH",
                    DollarValue = 0
                },
                new Coin
                {
                    Name = "Litecoin",
                    Amount = 0,
                    Symbol = "LTC",
                    DollarValue = 0
                },
        };

        public AssetsViewModelTests()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _mockWalletController = new Mock<IWalletController>();
        }

        [Fact]
        public async Task InitializeAsync_populates_assets()
        {
            _mockWalletController.GetCoinsReturns(_defaultAssets);
            AssetsViewModel viewModel = CreateAssetsViewModel();

            await viewModel.InitializeAsync(null);

            viewModel.Assets.Should().HaveCount(3);
            viewModel.Assets[0].DollarValue.Should().Be(11000);
        }

        [Fact]
        public void AddTransaction_navigates_to_the_add_transaction_view()
        {
            AssetsViewModel viewModel = CreateAssetsViewModel();

            viewModel.AddTransactionCommand.Execute(null);

            _mockNavigationService.VerifyThatPushAsyncWasCalled<AddTransactionViewModel>();
        }

        private AssetsViewModel CreateAssetsViewModel()
        {
            return new AssetsViewModel(_mockNavigationService.Object, _mockWalletController.Object);
        }
    }
}
