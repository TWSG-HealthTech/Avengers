using System;
using Machine.Specifications;
using Moq;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Common.Navigation;
using PowerPuff.Features.Medication.Services;
using PowerPuff.Features.Medication.Models;
using It = Machine.Specifications.It;

namespace PowerPuff.Features.Medication.Tests.Services
{
    [Subject(typeof(MedicationScheduleService))]
    public class MedicationScheduleServiceTests
    {
        private static Mock<INavigator> _navigator;
        private static Mock<IScheduler> _scheduler;
        private static MedicationScheduleService _subject;

        Establish context = () =>
        {
            _navigator = new Mock<INavigator>();
            _scheduler = new Mock<IScheduler>();
            _subject = new MedicationScheduleService(_navigator.Object, _scheduler.Object);
        };

        class When_set_schedule
        {
            private static Schedule _schedule = Schedule.Lunch;
            private static DateTime _time = DateTime.Now;

            Because of = () => _subject.SetSchedule(_schedule, _time);

            It should_set_schedule_with_scheduler =
                () => _scheduler.Verify(s => s.SetSchedule(_schedule, _time));
        }

        class When_schedule_is_triggered
        {
            Because of = () => _scheduler.Raise(s => s.OnSchedule += null, Schedule.Morning);

            It should_update_current_schedule = () => _subject.CurrentSchedule.ShouldEqual(Schedule.Morning);

            It should_navigate_to_medication_reminder_view =
                () => _navigator.Verify(n => n.GoToPage(NavigableViews.Medication.ReminderView.GetFullName()));
        }
    }
}
