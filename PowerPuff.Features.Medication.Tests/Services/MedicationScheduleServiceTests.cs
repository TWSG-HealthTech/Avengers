using System;
using System.Collections.Generic;
using Machine.Specifications;
using Moq;
using PowerPuff.Common.Helpers;
using PowerPuff.Features.Medication.Models;
using PowerPuff.Features.Medication.Services;
using It = Machine.Specifications.It;

namespace PowerPuff.Features.Medication.Tests.Services
{
    [Subject(typeof(MedicationScheduleService))]
    public class MedicationScheduleServiceTests
    {
        private static Mock<IJobScheduler> _schedulerMock;
        private static Mock<IDateTimeProvider> _dateTimeProviderMock;
        private static MedicationScheduleService _subject;
        private static MedicationSchedule _schedule;
        private static IList<MedicationSchedule> _activatedSchedules;
        private static DateTime _now;
        private static DateTime _expectedFirstRunTime;

        private Establish context = () =>
        {
            _schedulerMock = new Mock<IJobScheduler>();
            _dateTimeProviderMock = new Mock<IDateTimeProvider>();
            _subject = new MedicationScheduleService(_schedulerMock.Object, _dateTimeProviderMock.Object);
            _activatedSchedules = new List<MedicationSchedule>();
            _subject.OnMedicationSchedule += s => _activatedSchedules.Add(s);
            _schedule = new MedicationSchedule
            {
                Name = "Morning",
                Hour = 12,
                Minute = 30
            };
        };

        private class When_add_schedule_whose_time_is_before_now
        {
            private Establish context = () =>
            {
                _now = new DateTime(1990, 11, 11, 14, 0, 0);
                _expectedFirstRunTime = new DateTime(1990, 11, 12, 12, 30, 0);
                _dateTimeProviderMock.Setup(p => p.Now).Returns(_now);
            };

            private Because of = () => _subject.AddSchedule(_schedule);

            private It should_has_new_schedule = () => _subject.GetAllSchedules().ShouldContain(_schedule);

            private It should_schedule_new_job_starting_from_next_day =
                () =>
                    _schedulerMock.Verify(
                        s => s.AddDailySchedule(_schedule.Name, _expectedFirstRunTime));
        }

        private class When_add_schedule_whose_time_is_after_now
        {
            private Establish context = () =>
            {
                _now = new DateTime(1990, 11, 11, 10, 20, 0);
                _expectedFirstRunTime = new DateTime(1990, 11, 11, 12, 30, 0);
                _dateTimeProviderMock.Setup(p => p.Now).Returns(_now);
            };

            private Because of = () => _subject.AddSchedule(_schedule);

            private It should_has_new_schedule = () => _subject.GetAllSchedules().ShouldContain(_schedule);

            private It should_schedule_new_job_starting_from_same_day =
                () =>
                    _schedulerMock.Verify(
                        s => s.AddDailySchedule(_schedule.Name, _expectedFirstRunTime));
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