using System;
using Autofac;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Features.Medication.Models;
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
            RunModuleInitialTasks();
        }

        private void RunModuleInitialTasks()
        {
            var service = _container.Resolve<IMedicationScheduleService>();
            service.OnMedicationSchedule +=
                schedule =>
                    Console.WriteLine(
                        $"{DateTime.Now} Time to take medication: {schedule.Name} {schedule.TimeInDay} {schedule.frequencies}");
            service.AddSchedule(new MedicationSchedule() {Name = "Lunch", TimeInDay = DateTime.Now.AddMinutes(1)});
        }

        private void ConfigureDependencies()
        {
            var updater = new ContainerBuilder();

            updater.RegisterType<PrescriptionService>().As<IPrescriptionService>();
            updater.RegisterType<MedicationScheduleService>().As<IMedicationScheduleService>().SingleInstance();
            updater.RegisterType<JobScheduler>().As<IJobScheduler>();
            updater.RegisterTypeForNavigation<MedicationMainView>(NavigableViews.Medication.MainView.GetFullName());

            updater.Update(_container);
        }

        private void ConfigureViews()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.MainButtonsRegion, typeof(MedicationMainButtonView));
            ViewModelLocationProvider.SetDefaultViewModelFactory(type => _container.Resolve(type));
        }

    }
}