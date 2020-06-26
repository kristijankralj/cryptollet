using System;
using System.Reflection;
using Autofac;
using Cryptollet.Common.Navigation;
using Cryptollet.Modules.Onboarding;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cryptollet
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //class used for registration
            var builder = new ContainerBuilder();
            //scan and register all classes in the assembly
            var dataAccess = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(dataAccess)
                   .AsImplementedInterfaces()
                   .AsSelf();

            //register navigation service
            NavigationPage navigationPage = null;
            Func<INavigation> navigationFunc = () => {
                return navigationPage.Navigation;
            };
            builder.RegisterType<NavigationService>().As<INavigationService>()
                .WithParameter("navigation", navigationFunc)
                .SingleInstance();

            //get container
            var container = builder.Build();
            //set first page
            navigationPage = new NavigationPage(container.Resolve<OnboardingView>());
            MainPage = navigationPage;
        }
    }
}
