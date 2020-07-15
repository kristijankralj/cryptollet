
using Autofac;
using Xamarin.Forms;

namespace Cryptollet.Modules.AddAsset
{
    public partial class AddAssetView : ContentPage
    {
        public AddAssetView()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<AddAssetViewModel>();
        }
    }
}
