using System;
using System.Collections.ObjectModel;
using Cryptollet.Common.Base;
using Cryptollet.Common.Models;

namespace Cryptollet.Modules.Onboarding
{
    public class OnboardingViewModel: BaseViewModel
    {
        public ObservableCollection<OnboardingItem> OnboardingSteps { get; set; } = new ObservableCollection<OnboardingItem>
        {
            new OnboardingItem("1_welcome.png",
                "Welcome to Cryptollet",
                "Manage all your crypto assets! It's simple and easy!"),
            new OnboardingItem("2_nice.png",
                "Nice and Tidy Crypto Portfolio",
                "Keep BTC, ETH, XRP, and many other tokens."),
            new OnboardingItem("3_security.png",
                "Your Safety is Our Top Priority",
                "Our top-notch security features will keep you completely safe.")
        };
    }
}
