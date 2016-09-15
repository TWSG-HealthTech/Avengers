using PowerPuff.Features.Medication.ViewModels;

namespace PowerPuff.Features.Medication.Views
{
    /// <summary>
    /// Interaction logic for MedicationMainView.xaml
    /// </summary>
    public partial class MedicationMainView
    {
        public MedicationMainView(MedicationMainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
