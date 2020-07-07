using System;
using System.Collections.ObjectModel;
using Cryptollet.Common.Base;
using Cryptollet.Common.Models;

namespace Cryptollet.Modules.AddAsset
{
    public class AddAssetViewModel: BaseViewModel
    {
        public AddAssetViewModel()
        {
            AvailableAssets = new ObservableCollection<Coin>(Coin.GetAvailableAssets());
        }

        private ObservableCollection<Coin> _availableAssets;
        public ObservableCollection<Coin> AvailableAssets
        {
            get => _availableAssets;
            set { SetProperty(ref _availableAssets, value); }
        }
    }
}
