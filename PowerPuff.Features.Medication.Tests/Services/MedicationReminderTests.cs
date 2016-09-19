using Machine.Specifications;
using It = Machine.Specifications.It;
using Moq;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Common.Navigation;
using PowerPuff.Features.Medication.Models;
using PowerPuff.Features.Medication.Services;
using PowerPuff.Features.Medication.ViewModels;

namespace PowerPuff.Features.Medication.Tests.Services
{
    [Subject(typeof(MedicationReminder))]
    public class MedicationReminderTests
    {
        private static MedicationReminder _subject;
        private static Mock<INavigator> _navigatorMock;
        private static Mock<IMedicationScheduleService> _scheduleServiceMock;
        private static MedicationSchedule _schedule;

        Establish context = () =>
        {
            _navigatorMock = new Mock<INavigator>();
            _scheduleServiceMock = new Mock<IMedicationScheduleService>();
            _subject = new MedicationReminder(_navigatorMock.Object, _scheduleServiceMock.Object);
        };

        Because of = () => _scheduleServiceMock.Raise(s => s.OnMedicationSchedule += null, _schedule);

        It should_navigate_to_reminder_view =
            () => _navigatorMock.Verify(n => n.GoToPage(NavigableViews.Medication.ReminderView.GetFullName()));
    }
}