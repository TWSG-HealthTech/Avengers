using Machine.Specifications;
using Moq;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Common.Navigation;
using PowerPuff.Features.Medication.Views;
using It = Machine.Specifications.It;

namespace PowerPuff.Features.Medication.Tests.ViewModels
{
    class MedicationMainButtonViewModelTests
    {
        [Subject(typeof(MedicationMainButtonViewModel))]
        class When_medication_button_is_clicked
        {
            Establish context = () =>
            {
                _navigatorMock = new Mock<INavigator>();
                _subject = new MedicationMainButtonViewModel(_navigatorMock.Object);
            };

            Because of = () => _subject.GoToMedicationPageCommand.Execute();

            It navigates_to_medication_view =
                () =>
                    _navigatorMock.Verify(
                        navigator => navigator.GoToPage(NavigableViews.Medication.MainView.GetFullName()));

            private static Mock<INavigator> _navigatorMock;
            private static MedicationMainButtonViewModel _subject;
        }

    }
}
