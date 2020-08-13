using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptollet.Common.Controllers;
using Cryptollet.Common.Models;
using Moq;

namespace Cryptollet.Tests.Mocks
{
    public static class WalletControllerExtensions
    {
        public static void GetCoinsReturns(this Mock<IWalletController> mock, List<Coin> coins)
        {
            mock.Setup(x => x.GetCoins(It.IsAny<bool>()))
                .Returns(Task.FromResult(coins));
        }

        public static void GetTransactionsReturns(this Mock<IWalletController> mock, List<Transaction> transactions)
        {
            mock.Setup(x => x.GetTransactions(It.IsAny<bool>()))
                .Returns(Task.FromResult(transactions));
        }
    }
}
