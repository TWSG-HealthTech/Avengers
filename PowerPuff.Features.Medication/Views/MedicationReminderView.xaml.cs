using PowerPuff.Features.Medication.ViewModels;

namespace PowerPuff.Features.Medication.Views
{
    /// <summary>
    ///     Interaction logic for MedicationReminderView.xaml
    /// </summary>
    public partial class MedicationReminderView
    {
        public MedicationReminderView(MedicationReminderViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}