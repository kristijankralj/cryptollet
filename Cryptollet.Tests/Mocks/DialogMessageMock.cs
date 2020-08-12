using System;
using System.Threading.Tasks;
using Cryptollet.Common.Dialog;
using Moq;

namespace Cryptollet.Tests.Mocks
{
    public static class DialogMessageExtensions
    {
        public static void DisplayAlertReturns(this Mock<IDialogMessage> mock, bool alertResult)
        {
            mock.Setup(x => x.DisplayAlert(It.IsAny<string>(),
                                      It.IsAny<string>(),
                                      It.IsAny<string>(),
                                      It.IsAny<string>()))
                .Returns(Task.FromResult(alertResult));
        }

        public static void VerifyThatDisplayAlertWasCalledWithMessage(this Mock<IDialogMessage> mock, string message)
        {
            mock.Verify(x => x.DisplayAlert(It.IsAny<string>(), message, It.IsAny<string>()), Times.Once);
        }
    }
}
