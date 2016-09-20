using System;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Common.Navigation;
using PowerPuff.Features.Medication.Models;
using PowerPuff.Features.Medication.ViewModels;

namespace PowerPuff.Features.Medication.Services
{
    public class MedicationReminder
    {
        private readonly IMedicationScheduleService _medicationScheduleService;
        private readonly INavigator _navigator;

        public MedicationSchedule ActiveSchedule { get; set; }

        public MedicationReminder(INavigator navigator, IMedicationScheduleService medicationScheduleService)
        {
            _navigator = navigator;
            _medicationScheduleService = medicationScheduleService;
            _medicationScheduleService.OnMedicationSchedule += _scheduleService_OnMedicationSchedule;

            _medicationScheduleService.AddSchedule(new MedicationSchedule() { Name = "Morning", Hour = 8, Minute = 30, Frequencies = new[] { "2", "3" } });
            _medicationScheduleService.AddSchedule(new MedicationSchedule() { Name = "Lunch", Hour = 12, Minute = 0, Frequencies = new[] { "1", "3" } });
            _medicationScheduleService.AddSchedule(new MedicationSchedule() { Name = "Dinner", Hour = 19, Minute = 0, Frequencies = new[] { "2", "3" } });
        }

        private void _scheduleService_OnMedicationSchedule(MedicationSchedule schedule)
        {
            ActiveSchedule = schedule;
            _navigator.GoToPage(NavigableViews.Medication.ReminderView.GetFullName());
        }
    }

}