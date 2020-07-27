
using Autofac;
using Xamarin.Forms;

namespace Cryptollet.Modules.AddTransaction
{
    public partial class AddTransactionView : ContentPage
    {
        public AddTransactionView()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<AddTransactionViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as AddTransactionViewModel).InitializeAsync(null);
        }
    }
}
