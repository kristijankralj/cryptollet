using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptollet.Common.Database;
using Cryptollet.Common.Models;
using Moq;

namespace Cryptollet.Tests.Mocks
{
    public static class UserRepositoryExtensions
    {
        public static void GetAllAsyncReturns(this Mock<IRepository<User>> mock, List<User> users)
        {
            mock.Setup(x => x.GetAllAsync())
                .Returns(Task.FromResult(users));
        }

        public static void VerifyThatSaveAsyncWasCalled(this Mock<IRepository<User>> mock)
        {
            mock.Verify(x => x.SaveAsync(It.IsAny<User>()), Times.Once);
        }
    }
}
