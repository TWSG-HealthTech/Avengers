using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Machine.Specifications;
using It = Machine.Specifications.It;
using Moq;
using PowerPuff.Common.Navigation;
using PowerPuff.Features.Medication.Models;
using PowerPuff.Features.Medication.Services;
using PowerPuff.Features.Medication.ViewModels;

namespace PowerPuff.Features.Medication.Tests.ViewModels
{
    [Subject(typeof(MedicationReminderViewModel))]
    public class MedicationReminderViewModelTests
    {
        private static MedicationReminderViewModel _subject;
        private static Mock<IPrescriptionService> _prescriptionServiceMock;
        private static MedicationReminder _reminder;
        private static IEnumerable<DrugOrder> _drugs;

        Establish context = () =>
        {
            _prescriptionServiceMock = new Mock<IPrescriptionService>();
            _reminder = new MedicationReminder(new Mock<INavigator>().Object, new Mock<IMedicationScheduleService>().Object);
            _subject = new MedicationReminderViewModel(_prescriptionServiceMock.Object, _reminder);
        };

        class When_load_medication_reminder_view
        {
            Establish context = () =>
            {
                _drugs = new[] {new DrugOrder(), new DrugOrder()};
                _prescriptionServiceMock.Setup(
                    ps =>
                        ps.GetDrugsToTake(Moq.It.IsAny<string>(), Moq.It.IsAny<DateTime>(),
                            Moq.It.IsAny<IEnumerable<string>>())).Returns(Task.FromResult(_drugs));
                _reminder.ActiveSchedule = new MedicationSchedule();
            };

            Because of = () => _subject.Load().Wait();

            It should_load_drugs_to_take_now = () => _subject.DrugsToTake.ShouldContainOnly(_drugs);
        }
    }
}
