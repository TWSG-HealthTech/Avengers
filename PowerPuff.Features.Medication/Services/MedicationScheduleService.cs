using System;
using FluentScheduler;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Common.Navigation;
using PowerPuff.Features.Medication.ViewModels;
using Schedule = PowerPuff.Features.Medication.Models.Schedule;

namespace PowerPuff.Features.Medication.Services
{
    public class MedicationScheduleService : IMedicationScheduleService
    {
        private readonly INavigator _navigator;
        private readonly IScheduler _scheduler;

        public Schedule CurrentSchedule { get; set; }

        public MedicationScheduleService(INavigator navigator, IScheduler scheduler)
        {
            _navigator = navigator;
            _scheduler = scheduler;
            _scheduler.OnSchedule += _scheduler_OnSchedule;
        }

        private void _scheduler_OnSchedule(Schedule schedule)
        {
            CurrentSchedule = schedule;
            _navigator.GoToPage(NavigableViews.Medication.ReminderView.GetFullName());
        }

        public void SetSchedule(Schedule schedule, DateTime time)
        {
            _scheduler.SetSchedule(schedule, time);
        }

    }

    public class Scheduler : IScheduler
    {
        public event Action<Schedule> OnSchedule;
        public void SetSchedule(Schedule schedule, DateTime time)
        {
            JobManager.RemoveJob(schedule.GetFullName());
            JobManager.AddJob(() =>
            {
                OnSchedule?.Invoke(schedule);
            }, s => s.WithName(schedule.GetFullName()).ToRunOnceAt(time.Hour, time.Minute));
        }

    }

    public interface IScheduler
    {
        event Action<Schedule> OnSchedule;
        void SetSchedule(Schedule schedule, DateTime time);
    }
}
