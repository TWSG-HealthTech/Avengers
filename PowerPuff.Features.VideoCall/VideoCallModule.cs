using Autofac;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Common.Settings;
using PowerPuff.Features.VideoCall.Services;
using PowerPuff.Features.VideoCall.ViewModels;
using PowerPuff.Features.VideoCall.Views;
using Prism.Autofac;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;

namespace PowerPuff.Features.VideoCall
{
    public class VideoCallModule : IModule
    {
        private readonly IContainer _container;
        private readonly IRegionManager _regionManager;

        public VideoCallModule(IRegionManager regionManager, IContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            ConfigureDependencies();

            _regionManager.RegisterViewWithRegion(RegionNames.MainButtonsRegion, typeof(MainButtonView));

            ConfigureSettingMenu();

            ViewModelLocationProvider.SetDefaultViewModelFactory(type => _container.Resolve(type));
        }

        private void ConfigureSettingMenu()
        {
            var settingsRepository = _container.Resolve<IMenuSettingsRepository>();
            settingsRepository.RegisterMenu("Video", NavigableViews.VideoCall.SettingsView.GetFullName());
        }

        private void ConfigureDependencies()
        {
            var updater = new ContainerBuilder();

            updater.RegisterType<MainButtonViewModel>();
            updater.RegisterType<SettingsViewModel>();
            updater.RegisterType<SkypeUriVideoCallService>().As<IVideoCallService>();
            updater.RegisterType<Gateway>().As<IGateway>();

            updater.RegisterTypeForNavigation<VideoMainView>(NavigableViews.VideoCall.MainView.GetFullName());
            updater.RegisterTypeForNavigation<SettingsView>(NavigableViews.VideoCall.SettingsView.GetFullName());

            updater.Update(_container);
        }
    }
}
