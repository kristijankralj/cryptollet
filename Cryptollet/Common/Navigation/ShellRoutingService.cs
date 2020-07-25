using System.Threading.Tasks;
using Cryptollet.Common.Base;
using Xamarin.Forms;

namespace Cryptollet.Common.Navigation
{
    public interface INavigationService
    {
        Task PushAsync<TViewModel>() where TViewModel : BaseViewModel;
        Task PopAsync();
        Task InsertAsRoot<TViewModel>() where TViewModel : BaseViewModel;
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

        public Task InsertAsRoot<TViewModel>() where TViewModel : BaseViewModel
        {
            return Shell.Current.GoToAsync("//" + typeof(TViewModel).Name);
        }

        public Task PopAsync()
        {
            return Shell.Current.Navigation.PopAsync();
        }

        public Task PushAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            return Shell.Current.GoToAsync(typeof(TViewModel).Name);
        }
    }
}
