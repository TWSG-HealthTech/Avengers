using System.Windows;
using Autofac;
using PowerPuff.Common.Logging;
using PowerPuff.Modules;
using Prism.Autofac;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;

namespace PowerPuff
{
    public class Bootstrapper : AutofacBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return ComponentContext.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            ComponentContext.Resolve<ActiveListenerModule>().Initialize();

            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainerBuilder(ContainerBuilder builder)
        {
            base.ConfigureContainerBuilder(builder);

            builder.RegisterType<ShellViewModel>();
            builder.RegisterType<NLogAdapter>().As<ILogger>();
            builder.RegisterType<ActiveListenerModule>();
            
            ViewModelLocationProvider.SetDefaultViewModelFactory(type => Container.Resolve(type));
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog
            {
                ModulePath = @".\Features"
            };
        }

        private IComponentContext ComponentContext => Container;
    }
}
