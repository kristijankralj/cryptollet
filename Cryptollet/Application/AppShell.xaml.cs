using Autofac;
using Cryptollet.Modules.AddAsset;
using Xamarin.Forms;

namespace Cryptollet
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<AppShellViewModel>();

            Routing.RegisterRoute("AddAssetViewModel", typeof(AddAssetView));
        }
    }
}
