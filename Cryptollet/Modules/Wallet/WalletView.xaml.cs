using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Cryptollet.Modules.Wallet
{
    public partial class WalletView : ContentPage
    {
        public WalletView(WalletViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
