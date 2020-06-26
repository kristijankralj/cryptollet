using Autofac;
using Cryptollet.Common.Base;
using Cryptollet.Modules.Login;
using Cryptollet.Modules.Onboarding;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cryptollet.Common.Navigation
{
    public interface INavigationService
    {
        Task PushAsync<TViewModel>(object parameter = null) where TViewModel : BaseViewModel;
        Task PushAsync<TViewModel,TResult>(object parameter = null, EventHandler<TResult> onCompletion = null) where TViewModel : BaseViewModel, IViewModelCompletion<TResult>;
        Task PopAsync();
    }

    class NavigationService : INavigationService
    {
        private Func<INavigation> _navigation;
        private IComponentContext _container;
        private readonly Dictionary<Type, Type> _pageMap = new Dictionary<Type, Type>
        {
            // TODO: URL mapping goes here
            { typeof(OnboardingViewModel), typeof(OnboardingView) },
            { typeof(LoginViewModel), typeof(LoginView) },
        };

        public NavigationService(Func<INavigation> navigation, IComponentContext container)
        {
            _navigation = navigation;
            _container = container;
        }

        public async Task PopAsync()
        {
            await _navigation().PopAsync();
        }

        public async Task PushAsync<TViewModel>(object parameter = null) where TViewModel : BaseViewModel
        {
            Page page = await CreateAndPushPage<TViewModel>();
            await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);
        }

        private async Task<Page> CreateAndPushPage<TViewModel>() where TViewModel : BaseViewModel
        {
            var pageType = _pageMap[typeof(TViewModel)];
            Page page = _container.Resolve(pageType) as Page;
            await _navigation().PushAsync(page);
            return page;
        }

        public async Task PushAsync<TViewModel, TResult>(object parameter = null, EventHandler<TResult> onCompletion = null) where TViewModel : BaseViewModel, IViewModelCompletion<TResult>
        {
            Page page = await CreateAndPushPage<TViewModel>();
            await(page.BindingContext as BaseViewModel).InitializeAsync(parameter);
            (page.BindingContext as IViewModelCompletion<TResult>).Completed += onCompletion;
        }
    }
}
