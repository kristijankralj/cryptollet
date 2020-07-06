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
            AvailableAssets = new ObservableCollection<AvailableAsset>(AvailableAsset.GetAvailableAssets());
        }

        private ObservableCollection<AvailableAsset> _availableAssets;
        public ObservableCollection<AvailableAsset> AvailableAssets
        {
            get => _availableAssets;
            set { SetProperty(ref _availableAssets, value); }
        }
    }
}
