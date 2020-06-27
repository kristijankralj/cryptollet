using Xamarin.Forms;

namespace Cryptollet.Modules.Register
{
    public partial class RegisterView : ContentPage
    {
        public RegisterView(RegisterViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
