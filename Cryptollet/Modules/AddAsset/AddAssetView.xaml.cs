using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Cryptollet.Modules.AddAsset
{
    public partial class AddAssetView : ContentPage
    {
        public AddAssetView(AddAssetViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
