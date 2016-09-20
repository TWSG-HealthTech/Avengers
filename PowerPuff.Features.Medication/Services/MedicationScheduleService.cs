using System;
using System.Collections.Generic;
using FluentScheduler;
using PowerPuff.Features.Medication.Models;
using PowerPuff.Features.Medication.ViewModels;

namespace PowerPuff.Features.Medication.Services
{
    public class MedicationScheduleService : IMedicationScheduleService
    {
        private readonly IJobScheduler _scheduler;
        private readonly Dictionary<string, MedicationSchedule> _scheduleStore;

        public MedicationScheduleService(IJobScheduler scheduler)
        {
            _scheduler = scheduler;
            _scheduler.OnSchedule += _scheduler_OnSchedule;
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
            _scheduler.AddDailySchedule(schedule.Name, schedule.Hour, schedule.Minute);
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

        public void AddDailySchedule(string id, int hour, int minute)
        {
            JobManager.AddJob(() => OnSchedule?.Invoke(id),
                s => s.WithName(id).ToRunOnceAt(hour, minute).AndEvery(24).Hours());
        }

        public void RemoveDailySchedule(string id)
        {
            JobManager.RemoveJob(id);
        }
    }

    public interface IJobScheduler
    {
        event Action<string> OnSchedule;
        void AddDailySchedule(string id, int hour, int minute);
        void RemoveDailySchedule(string id);
    }
}