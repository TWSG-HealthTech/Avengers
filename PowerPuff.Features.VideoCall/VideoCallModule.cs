﻿using Autofac;
using PowerPuff.Common;
using PowerPuff.Features.VideoCall.Views;
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

            ViewModelLocationProvider.SetDefaultViewModelFactory(type => _container.Resolve(type));
        }

        private void ConfigureDependencies()
        {
            var updater = new ContainerBuilder();

            //updater.RegisterType<MenuViewModel>();
            
            updater.Update(_container);
        }
    }
}