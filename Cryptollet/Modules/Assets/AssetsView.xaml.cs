
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
    }
}
