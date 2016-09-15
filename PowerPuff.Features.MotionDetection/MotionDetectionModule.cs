using Autofac;
using Prism.Modularity;
using Prism.Regions;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Common.Settings;
using PowerPuff.Features.MotionDetection.Views;
using Prism.Autofac;

namespace PowerPuff.Features.MotionDetection
{
    public class MotionDetectionModule : IModule
    {
        private readonly IContainer _container;
        private readonly IRegionManager _regionManager;

        public MotionDetectionModule(IRegionManager regionManager, IContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            ConfigureDependencies();
            ConfigureSettingMenu();
        }

        private void ConfigureDependencies()
        {
            var updater = new ContainerBuilder();

            updater.RegisterTypeForNavigation<SettingsView>(NavigableViews.MotionDetection.SettingsView.GetFullName());

            updater.Update(_container);
        }

        private void ConfigureSettingMenu()
        {
            var settingsRepository = _container.Resolve<IMenuSettingsRepository>();
            settingsRepository.RegisterMenu("Motion Detection", NavigableViews.MotionDetection.SettingsView.GetFullName());
        }
    }
}
