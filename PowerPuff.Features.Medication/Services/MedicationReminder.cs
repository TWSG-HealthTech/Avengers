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

        public MedicationReminder(INavigator navigator, IMedicationScheduleService medicationScheduleService)
        {
            _navigator = navigator;
            _medicationScheduleService = medicationScheduleService;
            _medicationScheduleService.OnMedicationSchedule += _scheduleService_OnMedicationSchedule;

            // TODO: maybe load schedules from settings
            // _medicationScheduleService.AddSchedule(new MedicationSchedule() { Name = "Lunch", TimeInDay = DateTime.Now.AddMinutes(1) });
        }

        private void _scheduleService_OnMedicationSchedule(MedicationSchedule schedule)
        {
            _navigator.GoToPage(NavigableViews.Medication.ReminderView.GetFullName());
        }
    }
}