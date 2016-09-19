using System.Collections.Generic;
using Machine.Specifications;
using Moq;
using PowerPuff.Features.Medication.Models;
using PowerPuff.Features.Medication.ViewModels;
using It = Machine.Specifications.It;

namespace PowerPuff.Features.Medication.Tests.ViewModels
{
    [Subject(typeof(MedicationMainViewModel))]
    class MedicationMainViewModelTests
    {
        private static Mock<IPrescriptionService> _prescriptionService;
        private static MedicationMainViewModel _subject;

        Establish context = () =>
        {
            _prescriptionService = new Mock<IPrescriptionService>();
            _prescriptionService.Setup(p => p.GetPrescriptionAsync(Moq.It.IsAny<string>())).ReturnsAsync(new Prescription() {DrugOrders = new List<DrugOrder>()});
            _subject = new MedicationMainViewModel(_prescriptionService.Object);
        };

        class On_initialization
        {
            It has_empty_medication_list = () => _subject.DrugOrders.ShouldBeEmpty();
            It should_be_loading = () => _subject.IsLoading.ShouldBeFalse();
        }

        class On_loading
        {
            Because of = () => _subject.LoadDrugOrders().Wait();

            It should_load_drugs_with_prescription_service = () => _prescriptionService.Verify(p => p.GetPrescriptionAsync(Moq.It.IsAny<string>()));
            It should_be_loaded = () => _subject.IsLoading.ShouldBeFalse();
        }
    }
}
