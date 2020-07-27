using System.Threading.Tasks;
using Cryptollet.Common.Base;
using Xamarin.Forms;

namespace Cryptollet.Common.Navigation
{
    public interface INavigationService
    {
        Task PushAsync<TViewModel>(string parameters = null) where TViewModel : BaseViewModel;
        Task PopAsync();
        Task InsertAsRoot<TViewModel>(string parameters = null) where TViewModel : BaseViewModel;
        void GoToMainFlow();
        void GoToLoginFlow();
    }

    public class ShellRoutingService: INavigationService
    {
        public void GoToMainFlow()
        {
            Application.Current.MainPage = new AppShell();
        }

        public void GoToLoginFlow()
        {
            Application.Current.MainPage = new LoginShell();
        }

        public Task PopAsync()
        {
            return Shell.Current.Navigation.PopAsync();
        }

        public Task InsertAsRoot<TViewModel>(string parameters = null) where TViewModel : BaseViewModel
        {
            return GoToAsync<TViewModel>("//", parameters);
        }

        public Task PushAsync<TViewModel>(string parameters = null) where TViewModel : BaseViewModel
        {
            return GoToAsync<TViewModel>("", parameters);
        }

        private Task GoToAsync<TViewModel>(string routePrefix, string parameters) where TViewModel : BaseViewModel
        {
            var route = routePrefix + typeof(TViewModel).Name;
            if (!string.IsNullOrWhiteSpace(parameters))
            {
                route += $"?{parameters}";
            }
            return Shell.Current.GoToAsync(route);
        }
    }
}
