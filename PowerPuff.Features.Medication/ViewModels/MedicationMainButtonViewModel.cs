using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Common.Navigation;
using Prism.Commands;

namespace PowerPuff.Features.Medication.Views
{
    public class MedicationMainButtonViewModel
    {
        public MedicationMainButtonViewModel(INavigator navigator)
        {
            GoToMedicationPageCommand = new DelegateCommand((() => navigator.GoToPage(NavigableViews.Medication.MainView.GetFullName())));
        }

        public DelegateCommand GoToMedicationPageCommand { get; private set; }
    }
}