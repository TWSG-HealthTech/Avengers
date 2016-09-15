using Machine.Specifications;
using PowerPuff.Features.Medication.Views;
using It = Machine.Specifications.It;

namespace PowerPuff.Features.Medication.Tests.ViewModels
{
    [Subject(typeof(MedicationMainViewModel))]
    class MedicationMainViewModelTests
    {
        private static MedicationMainViewModel _subject;

        Establish context = () =>
        {
            _subject = new MedicationMainViewModel();
        };

        class On_initialization
        {
            It has_empty_medication_list = () => _subject.DrugOrders.ShouldBeEmpty();
            It should_be_loading = () => _subject.IsLoading.ShouldBeTrue();
        }

    }
}
