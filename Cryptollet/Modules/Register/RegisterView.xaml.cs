using Autofac;
using Xamarin.Forms;

namespace Cryptollet.Modules.Register
{
    public partial class RegisterView : ContentPage
    {
        public RegisterView()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<RegisterViewModel>(); ;
        }
    }
}
