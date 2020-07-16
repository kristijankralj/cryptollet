
using Autofac;
using Xamarin.Forms;

namespace Cryptollet.Modules.Transactions
{
    public partial class WithdrawnTransactionsView : ContentPage
    {
        public WithdrawnTransactionsView()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<WithdrawnTransactionsViewModel>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as WithdrawnTransactionsViewModel).InitializeAsync(null);
        }
    }
}
