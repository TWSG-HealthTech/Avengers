using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Machine.Specifications;
using It = Machine.Specifications.It;
using Moq;
using PowerPuff.Features.Medication.Models;
using PowerPuff.Features.Medication.ViewModels;

namespace PowerPuff.Features.Medication.Tests.ViewModels
{
    [Subject(typeof(MedicationSettingsViewModel))]
    class MedicationSettingsViewModelTests
    {
        private static MedicationSettingsViewModel _subject;
        private static Mock<IMedicationScheduleService> _scheduleServiceMock;
        private static MedicationSchedule _schedule;
        private static IList<MedicationSchedule> _removedSchedule;
        private static IList<MedicationSchedule> _addedSchedule;

        private Establish context = () =>
        {
            _schedule = new MedicationSchedule() {Name = "Test", Hour = 1, Minute = 1, Frequencies = new [] {"1"}};
            _removedSchedule = new List<MedicationSchedule>();
            _addedSchedule = new List<MedicationSchedule>();

            _scheduleServiceMock = new Mock<IMedicationScheduleService>();
            _scheduleServiceMock.Setup(s => s.GetAllSchedules()).Returns(new[] {_schedule});
            _scheduleServiceMock.Setup(s => s.AddSchedule(Moq.It.IsAny<MedicationSchedule>()))
                .Callback<MedicationSchedule>(ms => _addedSchedule.Add(ms));
            _scheduleServiceMock.Setup(s => s.RemoveSchedule(Moq.It.IsAny<MedicationSchedule>()))
                .Callback<MedicationSchedule>(ms => _removedSchedule.Add(ms));

            _subject = new MedicationSettingsViewModel(_scheduleServiceMock.Object);
        };

        Because of = () => _subject.UpdateMedicationSchedules.Execute();

        It should_remove_existing_schedule =
            () => _removedSchedule.ShouldContainOnly(_schedule);

        It should_add_new_schedule =
            () => _addedSchedule.ShouldContainOnly(_schedule);
    }
}
