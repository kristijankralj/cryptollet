
using Xamarin.Forms;

namespace Cryptollet.Modules.Transactions
{
    public partial class TransactionsView : ContentPage
    {
        public TransactionsView(TransactionsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as TransactionsViewModel).InitializeAsync(null);
        }
    }
}
