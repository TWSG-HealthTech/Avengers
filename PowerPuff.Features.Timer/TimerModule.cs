using Autofac;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Features.Timer.ViewModels;
using PowerPuff.Features.Timer.Views;
using Prism.Autofac;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;

namespace PowerPuff.Features.Timer
{
    public class TimerModule : IModule
    {
        private readonly IContainer _container;
        private readonly IRegionManager _regionManager;

        public TimerModule(IRegionManager regionManager, IContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            ConfigureDependencies();

            _regionManager.RegisterViewWithRegion(RegionNames.MainButtonsRegion, typeof(TimerMainButtonView));

            ViewModelLocationProvider.SetDefaultViewModelFactory(type => _container.Resolve(type));
        }

        private void ConfigureDependencies()
        {
            var updater = new ContainerBuilder();

            updater.RegisterType<TimerMainButtonViewModel>();

            updater.RegisterTypeForNavigation<TimerMainView>(NavigableViews.Timer.MainView.GetFullName());

            updater.Update(_container);
        }
    }
}
