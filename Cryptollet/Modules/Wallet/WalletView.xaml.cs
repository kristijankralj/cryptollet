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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var whiteColor = SKColor.Parse("#ffffff");
            var entries = new[]
            {
                new Microcharts.Entry(200)
                {
                    TextColor = whiteColor,
                    ValueLabel = "200",
                    Color = whiteColor,
                },
                new Microcharts.Entry(400)
                {
                    TextColor = whiteColor,
                    ValueLabel = "400",
                    Color = whiteColor,
                },
                new Microcharts.Entry(100)
                {
                    TextColor = whiteColor,
                    ValueLabel = "100",
                    Color = whiteColor,
                }
            };

            var chart = new LineChart() { Entries = entries };
            chart.LabelTextSize = 24;
            chart.BackgroundColor = SKColor.Parse("#347AF0");
            chartView.Chart = chart;
        }
    }
}
