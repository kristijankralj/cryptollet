using System;
using System.Threading.Tasks;
using Cryptollet.Common.Base;
using Xamarin.Forms;

namespace Cryptollet.Common.Navigation
{
    public class ShellRoutingService: INavigationService
    {
        public ShellRoutingService()
        {
        }

        public Task InsertAsRoot<TViewModel>(object parameter = null) where TViewModel : BaseViewModel
        {
            return Shell.Current.GoToAsync(typeof(TViewModel).Name);
        }

        public Task PopAsync()
        {
            return Shell.Current.Navigation.PopAsync();
        }

        public Task PushAsync<TViewModel>(object parameter = null) where TViewModel : BaseViewModel
        {
            return Shell.Current.GoToAsync("//" + typeof(TViewModel).Name);
        }
    }
}
