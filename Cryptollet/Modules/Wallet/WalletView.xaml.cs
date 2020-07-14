using Microcharts;
using SkiaSharp;
using Xamarin.Forms;

namespace Cryptollet.Modules.Wallet
{
    public partial class WalletView : ContentPage
    {
        public WalletView(WalletViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as WalletViewModel).LoadData();
        }
    }
}
