using System;
using System.Collections.Generic;
using FluentScheduler;
using PowerPuff.Common.Helpers;
using PowerPuff.Features.Medication.Models;
using PowerPuff.Features.Medication.ViewModels;

namespace PowerPuff.Features.Medication.Services
{
    public class MedicationScheduleService : IMedicationScheduleService
    {
        private readonly IJobScheduler _scheduler;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly Dictionary<string, MedicationSchedule> _scheduleStore;

        public MedicationScheduleService(IJobScheduler scheduler, IDateTimeProvider dateTimeProvider)
        {
            _scheduler = scheduler;
            _scheduler.OnSchedule += _scheduler_OnSchedule;
            _dateTimeProvider = dateTimeProvider;
            _scheduleStore = new Dictionary<string, MedicationSchedule>();
        }

        public event Action<MedicationSchedule> OnMedicationSchedule;

        private void _scheduler_OnSchedule(string scheduleId)
        {
            OnMedicationSchedule?.Invoke(_scheduleStore[scheduleId]);
        }

        public void AddSchedule(MedicationSchedule schedule)
        {
            _scheduleStore.Add(schedule.Name, schedule);
            _scheduler.AddDailySchedule(schedule.Name, CalculateFirstRunTime(schedule.Hour, schedule.Minute));
        }

        private DateTime CalculateFirstRunTime(int hour, int minute)
        {
            var now = _dateTimeProvider.Now;
            var scheduledTimeInSameDay = new DateTime(now.Year, now.Month, now.Day, hour, minute, 0);
            return scheduledTimeInSameDay > now
                ? scheduledTimeInSameDay
                : scheduledTimeInSameDay.AddDays(1);
        }

        public void RemoveSchedule(MedicationSchedule schedule)
        {
            _scheduleStore.Remove(schedule.Name);
            _scheduler.RemoveDailySchedule(schedule.Name);
        }

        public IEnumerable<MedicationSchedule> GetAllSchedules()
        {
            return _scheduleStore.Values;
        }
    }

    public class JobScheduler : IJobScheduler
    {
        public event Action<string> OnSchedule;

        private readonly IDateTimeProvider _dateTimeProvider;

        public JobScheduler(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public void AddDailySchedule(string id, DateTime firstRunTime)
        {
            JobManager.AddJob(() => OnSchedule?.Invoke(id),
                s => s.WithName(id).ToRunOnceAt(firstRunTime).AndEvery(24).Hours());
        }

        public void RemoveDailySchedule(string id)
        {
            JobManager.RemoveJob(id);
        }
    }

    public interface IJobScheduler
    {
        event Action<string> OnSchedule;
        void AddDailySchedule(string id, DateTime firstRunTime);
        void RemoveDailySchedule(string id);
    }
}