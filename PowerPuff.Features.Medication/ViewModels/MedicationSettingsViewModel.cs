using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PowerPuff.Features.Medication.Models;
using PowerPuff.Features.Medication.Services;
using Prism.Commands;

namespace PowerPuff.Features.Medication.ViewModels
{
    public class MedicationSettingsViewModel
    {
        private readonly IMedicationScheduleService _scheduleService;

        public List<MedicationSchedule> MedicationSchedules { get; set; }
        public IEnumerable<int> SelectableHours { get; set; }
        public IEnumerable<int> SelectableMinutes { get; set; }
        public DelegateCommand UpdateMedicationSchedules { get; private set; }

        public MedicationSettingsViewModel(IMedicationScheduleService scheduleService)
        {
            _scheduleService = scheduleService;

            MedicationSchedules = new List<MedicationSchedule>();
            MedicationSchedules.AddRange(_scheduleService.GetAllSchedules());

            SelectableHours = Enumerable.Range(0, 24).ToArray();
            SelectableMinutes = Enumerable.Range(0, 60).ToArray();

            UpdateMedicationSchedules = new DelegateCommand(_On_update_medication_schedules);
        }

        private void _On_update_medication_schedules()
        {
            foreach (var schedule in MedicationSchedules)
            {
                _scheduleService.RemoveSchedule(schedule);
                _scheduleService.AddSchedule(schedule);
            }
        }
    }
}
