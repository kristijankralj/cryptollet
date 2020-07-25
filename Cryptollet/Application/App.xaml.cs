using System.Reflection;
using Autofac;
using Cryptollet.Common.Database;
using Cryptollet.Common.Models;
using Cryptollet.Common.Navigation;
using Cryptollet.Modules.Loading;
using Xamarin.Forms;

namespace Cryptollet
{
    public partial class App : Application
    {
        public static IContainer Container;

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

            //get container
            Container = builder.Build();
            //set first page
            MainPage = Container.Resolve<LoadingView>();
        }
    }
}
