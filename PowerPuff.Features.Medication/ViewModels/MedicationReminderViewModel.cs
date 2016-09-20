using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PowerPuff.Features.Medication.Models;
using PowerPuff.Features.Medication.Services;
using Prism.Mvvm;
using Prism.Regions;

namespace PowerPuff.Features.Medication.ViewModels
{
    public class MedicationReminderViewModel : BindableBase, INavigationAware
    {
        private readonly IPrescriptionService _prescriptionService;
        private readonly MedicationReminder _reminder;

        public ObservableCollection<DrugOrder> DrugsToTake { get; private set; }

        public MedicationReminderViewModel(IPrescriptionService prescriptionService,
            MedicationReminder reminder)
        {
            _prescriptionService = prescriptionService;
            _reminder = reminder;
            DrugsToTake = new ObservableCollection<DrugOrder>();
        }

        public async Task Load()
        {
            var drugs = await _prescriptionService.GetDrugsToTake("patientId", DateTime.Now, _reminder.ActiveSchedule.Frequencies);
            DrugsToTake.AddRange(drugs);
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            await Load();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }
    }
}
