
using Xamarin.Forms;

namespace Cryptollet.Modules.Assets
{
    public partial class AssetsView : ContentPage
    {
        public AssetsView(AssetsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as AssetsViewModel).LoadData();
        }
    }
}
