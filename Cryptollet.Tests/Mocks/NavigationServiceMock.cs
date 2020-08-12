using System;
using Cryptollet.Common.Base;
using Cryptollet.Common.Navigation;
using Moq;

namespace Cryptollet.Tests.Mocks
{
    public static class NavigationServiceExtensions
    {
        public static void VerifyThatGoBackWasCalledOnce(this Mock<INavigationService> mock)
        {
            mock.Verify(x => x.GoBackAsync(), Times.Once);
        }

        public static void VerifyThatGoBackWasNotCalled(this Mock<INavigationService> mock)
        {
            mock.Verify(x => x.GoBackAsync(), Times.Never);
        }

        public static void VerifyThatPushAsyncWasCalled<TViewModel>(this Mock<INavigationService> mock) where TViewModel : BaseViewModel
        {
            mock.Verify(x => x.PushAsync<TViewModel>(It.IsAny<string>()), Times.Once);
        }

        public static void VerifyThatGoToLoginFlowWasCalled(this Mock<INavigationService> mock)
        {
            mock.Verify(x => x.GoToLoginFlow(), Times.Once);
        }

        public static void VerifyThatGoToMainFlowWasCalled(this Mock<INavigationService> mock)
        {
            mock.Verify(x => x.GoToMainFlow(), Times.Once);
        }

        public static void VerifyThatInsertAsRootWasNotCalled<TViewModel>(this Mock<INavigationService> mock) where TViewModel: BaseViewModel
        {
            mock.Verify(x => x.InsertAsRoot<TViewModel>(It.IsAny<string>()), Times.Never);
        }

        public static void VerifyThatInsertAsRootWasCalled<TViewModel>(this Mock<INavigationService> mock) where TViewModel : BaseViewModel
        {
            mock.Verify(x => x.InsertAsRoot<TViewModel>(It.IsAny<string>()), Times.Once);
        }
    }
}
