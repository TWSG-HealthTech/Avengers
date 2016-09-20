using Autofac;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Common.Settings;
using PowerPuff.Features.Medication.Services;
using PowerPuff.Features.Medication.ViewModels;
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
            ConfigureViews();
            ConfigureSettingsMenu();

            _container.Resolve<MedicationReminder>();
        }

        private void ConfigureDependencies()
        {
            var updater = new ContainerBuilder();

            updater.RegisterType<PrescriptionFileCache>().As<IPrescriptionCache>();
            updater.RegisterType<PrescriptionService>().As<IPrescriptionService>();
            updater.RegisterType<MedicationScheduleService>().As<IMedicationScheduleService>().SingleInstance();
            updater.RegisterType<JobScheduler>().As<IJobScheduler>();
            updater.RegisterType<MedicationReminder>().SingleInstance();
            updater.RegisterTypeForNavigation<MedicationMainView>(NavigableViews.Medication.MainView.GetFullName());
            updater.RegisterTypeForNavigation<MedicationReminderView>(NavigableViews.Medication.ReminderView.GetFullName());
            updater.RegisterTypeForNavigation<MedicationSettingsView>(NavigableViews.Medication.SettingsView.GetFullName());

            updater.Update(_container);
        }

        private void ConfigureViews()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.MainButtonsRegion, typeof(MedicationMainButtonView));
            ViewModelLocationProvider.SetDefaultViewModelFactory(type => _container.Resolve(type));
        }

        private void ConfigureSettingsMenu()
        {
            var settingsRepository = _container.Resolve<IMenuSettingsRepository>();
            settingsRepository.RegisterMenu("Medication", NavigableViews.Medication.SettingsView.GetFullName());
        }

    }
}