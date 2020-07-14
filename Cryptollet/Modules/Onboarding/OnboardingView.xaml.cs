using Autofac;
using Xamarin.Forms;

namespace Cryptollet.Modules.Onboarding
{
    public partial class OnboardingView : ContentPage
    {
        public OnboardingView()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<OnboardingViewModel>();
        }
    }
}
