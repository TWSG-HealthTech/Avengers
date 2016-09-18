using System;
using System.Collections.Generic;
using Machine.Specifications;
using Moq;
using PowerPuff.Features.Medication.Models;
using PowerPuff.Features.Medication.Services;
using It = Machine.Specifications.It;

namespace PowerPuff.Features.Medication.Tests.Services
{
    [Subject(typeof(MedicationScheduleService))]
    public class MedicationScheduleServiceTests
    {
        private static Mock<IJobScheduler> _schedulerMock;
        private static MedicationScheduleService _subject;
        private static MedicationSchedule _schedule;
        private static IList<MedicationSchedule> _activatedSchedules;

        private Establish context = () =>
        {
            _schedulerMock = new Mock<IJobScheduler>();
            _subject = new MedicationScheduleService(_schedulerMock.Object);
            _activatedSchedules = new List<MedicationSchedule>();
            _subject.OnMedicationSchedule += s => _activatedSchedules.Add(s);
            _schedule = new MedicationSchedule
            {
                Name = "Morning",
                TimeInDay = new DateTime(1, 1, 1, 12, 30, 0)
            };
        };

        private class When_add_schedule
        {
            private Because of = () => _subject.AddSchedule(_schedule);

            private It should_has_new_schedule = () => _subject.GetAllSchedules().ShouldContain(_schedule);

            private It should_schedule_new_job_with_job_scheduler =
                () =>
                    _schedulerMock.Verify(
                        s => s.AddDailySchedule(_schedule.Name, _schedule.TimeInDay.Hour, _schedule.TimeInDay.Minute));
        }

        private class When_schedule_is_triggered
        {
            private Establish context = () => { _subject.AddSchedule(_schedule); };

            private Because of = () => _schedulerMock.Raise(s => s.OnSchedule += null, _schedule.Name);

            private It should_publish_schedule_event = () => _activatedSchedules.ShouldContain(_schedule);
        }

        private class When_remove_schedule
        {
            private Establish context = () => { _subject.AddSchedule(_schedule); };

            private Because of = () => _subject.RemoveSchedule(_schedule);

            private It should_not_have_the_removed_schedule =
                () => _subject.GetAllSchedules().ShouldNotContain(_schedule);

            private It should_remove_job_from_job_scheduler =
                () => _schedulerMock.Verify(s => s.RemoveDailySchedule(_schedule.Name));
        }
    }
}