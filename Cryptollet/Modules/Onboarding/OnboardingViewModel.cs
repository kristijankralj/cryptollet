using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Cryptollet.Common.Base;
using Cryptollet.Common.Models;
using Cryptollet.Common.Navigation;
using Xamarin.Forms;

namespace Cryptollet.Modules.Onboarding
{
    public class OnboardingViewModel: BaseViewModel, IViewModelCompletion<object>
    {
        public event EventHandler<object> Completed;

        public ObservableCollection<OnboardingItem> OnboardingSteps { get; set; } = new ObservableCollection<OnboardingItem>
        {
            new OnboardingItem("welcome.png",
                "Welcome to Cryptollet",
                "Manage all your crypto assets! It's simple and easy!"),
            new OnboardingItem("nice.png",
                "Nice and Tidy Crypto Portfolio",
                "Keep BTC, ETH, XRP, and many other tokens."),
            new OnboardingItem("security.png",
                "Your Safety is Our Top Priority",
                "Our top-notch security features will keep you completely safe.")
        };

        public ICommand SkipCommand { get => new Command(FinishOnboarding); }

        private void FinishOnboarding()
        {
            Completed?.Invoke(null, null);
            Completed = null;
        }
    }
}
