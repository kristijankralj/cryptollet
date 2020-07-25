using Autofac;
using Xamarin.Forms;

namespace Cryptollet.Modules.Assets
{
    public partial class AssetsView : ContentPage
    {
        public AssetsView()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<AssetsViewModel>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as AssetsViewModel).InitializeAsync(null);
        }
    }
}
