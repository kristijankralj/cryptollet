using System;
using System.Reflection;
using Autofac;
using Cryptollet.Common.Database;
using Cryptollet.Common.Extensions;
using Cryptollet.Common.Models;
using Cryptollet.Common.Navigation;
using Cryptollet.Modules.Onboarding;
using Xamarin.Forms;

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
            builder.RegisterType<Repository<User>>().As<IRepository<User>>();
            builder.RegisterType<Repository<Transaction>>().As<IRepository<Transaction>>();

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
            navigationPage = new NavigationPage();
            MainPage = navigationPage;
            var coordinator = container.Resolve<InitialFlowCoordinator>();
            coordinator.Start().SafeFireAndForget(false);
        }
    }
}
