using System;
using System.Collections.Generic;
using Cryptollet.Modules.AddAsset;
using Cryptollet.Modules.Assets;
using Cryptollet.Modules.Login;
using Cryptollet.Modules.Onboarding;
using Cryptollet.Modules.Register;
using Cryptollet.Modules.Transactions;
using Cryptollet.Modules.Wallet;

namespace Cryptollet.Common.Navigation
{
    public static class Router
    {
        public static Dictionary<Type, Type> GetRoutes()
        {
            return new Dictionary<Type, Type>
                {
                    //Navigation mapping goes here
                    { typeof(OnboardingViewModel), typeof(OnboardingView) },
                    { typeof(LoginViewModel), typeof(LoginView) },
                    { typeof(RegisterViewModel), typeof(RegisterView) },
                    { typeof(WalletViewModel), typeof(WalletView) },
                    { typeof(AssetsViewModel), typeof(AssetsView) },
                    { typeof(TransactionsViewModel), typeof(TransactionsView) },
                    { typeof(AddAssetViewModel), typeof(AddAssetView) },
                };
        }
    }
}
