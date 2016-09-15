﻿using Autofac;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Features.Medication.Views;
using Prism.Autofac;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;

namespace PowerPuff.Features.Medication
{
    public class MedicationModule : IModule
    {
        private readonly IContainer _container;
        private readonly IRegionManager _regionManager;

        public MedicationModule(IRegionManager regionManager, IContainer container)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            ConfigureDependencies();

            _regionManager.RegisterViewWithRegion(RegionNames.MainButtonsRegion, typeof(MedicationMainButtonView));

            ViewModelLocationProvider.SetDefaultViewModelFactory(type => _container.Resolve(type));
        }

        private void ConfigureDependencies()
        {
            var updater = new ContainerBuilder();

            updater.RegisterTypeForNavigation<MedicationMainView>(NavigableViews.Medication.MainView.GetFullName());

            updater.Update(_container);
        }
    }
}