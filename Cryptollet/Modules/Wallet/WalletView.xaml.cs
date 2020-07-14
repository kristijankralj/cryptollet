using Autofac;
using Xamarin.Forms;

namespace Cryptollet.Modules.Wallet
{
    public partial class WalletView : ContentPage
    {
        public WalletView()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<WalletViewModel>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as WalletViewModel).LoadData();
        }
    }
}
