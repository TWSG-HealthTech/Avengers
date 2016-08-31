using System.Windows;
using Autofac;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Common.Logging;
using PowerPuff.Common.Prism;
using PowerPuff.Common.Settings;
using PowerPuff.Modules;
using PowerPuff.Services;
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

            ConfigureSettings();

            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
            Application.Current.Exit += (sender, args) => Container.Dispose();
        }

        private void ConfigureSettings()
        {
            var settingsRepository = ComponentContext.Resolve<IMenuSettingsRepository>();
            settingsRepository.RegisterMenu("Profile", NavigableViews.Main.ProfileSettingsView.GetFullName());
        }

        protected override void ConfigureContainerBuilder(ContainerBuilder builder)
        {
            base.ConfigureContainerBuilder(builder);

            builder.RegisterType<ShellViewModel>();
            builder.RegisterType<MainButtonsViewModel>();
            builder.RegisterType<SettingsViewModel>();
            builder.RegisterType<FeatureLayoutViewModel>();
            builder.RegisterType<ProfileSettingsViewModel>();
            builder.RegisterType<NLogAdapter>().As<ILogger>();
            builder.RegisterType<ActiveListenerModule>().SingleInstance();
            builder.RegisterType<ActiveListenerView>().As<IActiveListenerView>();
            builder.RegisterType<ActiveListener>().As<IActiveListener>().SingleInstance();
            builder.RegisterType<ApplicationSettings>().As<IApplicationSettings>().SingleInstance();

            builder.RegisterType<ProfileGateway>().As<IProfileGateway>();

            builder.RegisterType<MenuSettingsRepository>().As<IMenuSettingsRepository>().SingleInstance();

            builder.RegisterType<ScopedRegionNavigationContentLoader>().As<IRegionNavigationContentLoader>().SingleInstance();

            builder.RegisterTypeForNavigation<MainButtonsView>(NavigableViews.Main.HomeView.GetFullName());
            builder.RegisterTypeForNavigation<SettingsView>(NavigableViews.Main.SettingsView.GetFullName());            
            builder.RegisterTypeForNavigation<FeatureLayoutView>(NavigableViews.Main.FeatureLayoutView.GetFullName());            
            builder.RegisterTypeForNavigation<ProfileSettingsView>(NavigableViews.Main.ProfileSettingsView.GetFullName());            
            
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
