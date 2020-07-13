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
            await (BindingContext as WalletViewModel).InitializeAsync(null);
            var whiteColor = SKColor.Parse("#ffffff");
            var entries = new[]
            {
                new Microcharts.Entry(1903.51f)
                {
                    TextColor = whiteColor,
                    ValueLabel = "Ethereum",
                    Color = SKColor.Parse("#ba68c8"),
                },
                new Microcharts.Entry(9180.19f)
                {
                    TextColor = whiteColor,
                    ValueLabel = "Bicoin",
                    Color = SKColor.Parse("#4fc3f7"),
                },
                new Microcharts.Entry(1092.08f)
                {
                    TextColor = whiteColor,
                    ValueLabel = "Litecoin",
                    Color = SKColor.Parse("#dce775"),
                }
            };

            var chart = new DonutChart { Entries = entries };
            chart.LabelTextSize = 24;
            chart.BackgroundColor = whiteColor;
            chartView.Chart = chart;
        }
    }
}
