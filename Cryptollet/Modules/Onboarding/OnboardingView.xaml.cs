using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Cryptollet.Modules.Onboarding
{
    public partial class OnboardingView : ContentPage
    {
        public OnboardingView(OnboardingViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
