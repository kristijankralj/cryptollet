
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
    }
}
