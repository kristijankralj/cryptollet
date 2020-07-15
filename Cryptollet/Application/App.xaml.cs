using System;
using System.Reflection;
using Autofac;
using Cryptollet.Common.Database;
using Cryptollet.Common.Models;
using Cryptollet.Common.Navigation;
using Cryptollet.Modules.Loading;
using Cryptollet.Modules.Login;
using Cryptollet.Modules.Register;
using Xamarin.Forms;

namespace Cryptollet
{
    public partial class App : Application
    {
        public static IContainer Container;
        public static NavigationPage MainNavigation;

        public App()
        {
            InitializeComponent();
            //class used for registration
            var builder = new ContainerBuilder();
            //scan and register all classes in the assembly
            var dataAccess = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(dataAccess)
                   .Where(x => x.Namespace != "Navigation")
                   .AsImplementedInterfaces()
                   .AsSelf();
            builder.RegisterType<Repository<User>>().As<IRepository<User>>();
            builder.RegisterType<Repository<Transaction>>().As<IRepository<Transaction>>();
            builder.RegisterType<ShellRoutingService>().As<INavigationService>();

            Func<INavigationService> navigationServiceFunc = () =>
            {
                return Container.Resolve<NavigationService>();
            };
            builder.RegisterType<LoadingViewModel>()
                .AsSelf()
                .WithParameter("navigationService", navigationServiceFunc);
            builder.RegisterType<LoginViewModel>()
                .AsSelf()
                .WithParameter("navigationService", navigationServiceFunc);
            builder.RegisterType<RegisterViewModel>()
                .AsSelf()
                .WithParameter("navigationService", navigationServiceFunc);

            //register navigation service
            Func<INavigation> navigationFunc = () => {
                return MainNavigation.Navigation;
            };
            builder.RegisterType<NavigationService>()
                .AsSelf()
                .WithParameter("navigation", navigationFunc)
                .SingleInstance();

            //get container
            Container = builder.Build();
            //set first page
            MainNavigation = new NavigationPage(Container.Resolve<LoadingView>());
            MainPage = MainNavigation;
        }
    }
}
