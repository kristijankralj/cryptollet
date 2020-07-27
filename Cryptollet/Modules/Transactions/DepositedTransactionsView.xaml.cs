
using Autofac;
using Xamarin.Forms;

namespace Cryptollet.Modules.Transactions
{
    public partial class DepositedTransactionsView : ContentPage
    {
        public DepositedTransactionsView()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<DepositedTransactionsViewModel>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as DepositedTransactionsViewModel).InitializeAsync(null);
        }
    }
}
