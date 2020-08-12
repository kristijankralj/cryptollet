using System;
using Cryptollet.Common.Database;
using Cryptollet.Common.Models;
using Moq;

namespace Cryptollet.Tests.Mocks
{
    public static class TransactionRepositoryExtensions
    {
        public static void VerifyThatSaveAsyncWasCalled(this Mock<IRepository<Transaction>> mock)
        {
            mock.Verify(x => x.SaveAsync(It.IsAny<Transaction>()), Times.Once);
        }
    }
}
