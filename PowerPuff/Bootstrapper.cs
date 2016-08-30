using System.Windows;
using System.Windows.Controls;
using Autofac;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Common.Logging;
using PowerPuff.Common.Prism;
using PowerPuff.Common.Settings;
using PowerPuff.Modules;
using PowerPuff.Settings;
using PowerPuff.Speech;
using PowerPuff.ViewModels;
using PowerPuff.Views;
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

            var regionManager = ComponentContext.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.MainContentRegion, typeof(MainButtonsView));

            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainerBuilder(ContainerBuilder builder)
        {
            base.ConfigureContainerBuilder(builder);

            builder.RegisterType<ShellViewModel>();
            builder.RegisterType<MainButtonsViewModel>();
            builder.RegisterType<SettingsViewModel>();
            builder.RegisterType<FeatureLayoutViewModel>();
            builder.RegisterType<NLogAdapter>().As<ILogger>();
            builder.RegisterType<ActiveListenerModule>();
            builder.RegisterType<ActiveListenerView>().As<IActiveListenerView>();
            builder.RegisterType<ActiveListener>().As<IActiveListener>();
            builder.RegisterType<ApplicationSettings>().As<IApplicationSettings>();

            builder.RegisterType<SettingsRepository>().As<ISettingsRepository>().SingleInstance();

            builder.RegisterType<ScopedRegionNavigationContentLoader>().As<IRegionNavigationContentLoader>().SingleInstance();

            builder.RegisterTypeForNavigation<MainButtonsView>(NavigableViews.Main.HomeView.GetFullName());
            builder.RegisterTypeForNavigation<SettingsView>(NavigableViews.Main.SettingsView.GetFullName());            
            builder.RegisterTypeForNavigation<FeatureLayoutView>(NavigableViews.Main.FeatureLayoutView.GetFullName());            
            
            ViewModelLocationProvider.SetDefaultViewModelFactory(type => Container.Resolve(type));
        }

        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var behaviors = base.ConfigureDefaultRegionBehaviors();

            behaviors.AddIfMissing(RegionManagerAwareBehavior.BehaviorKey, typeof(RegionManagerAwareBehavior));

            return behaviors;
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
