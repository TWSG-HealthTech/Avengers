using System.Collections.Generic;
using System.Linq;
using PowerPuff.Features.Medication.Models;
using PowerPuff.Features.Medication.ViewModels;

namespace PowerPuff.Features.Medication.Views
{
    /// <summary>
    /// Interaction logic for MedicationSettingsView.xaml
    /// </summary>
    public partial class MedicationSettingsView
    {
        public MedicationSettingsView(MedicationSettingsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }

    public class FakeMedicationSettingsViewModel
    {
        public IList<MedicationSchedule> MedicationSchedules { get; set; }

        public FakeMedicationSettingsViewModel()
        {
            MedicationSchedules = new List<MedicationSchedule>()
            {
                new MedicationSchedule() {Name = "Morning", Hour = 8, Minute = 0},
                new MedicationSchedule() {Name = "Morning", Hour = 12, Minute = 0},
                new MedicationSchedule() {Name = "Morning", Hour = 18, Minute = 0}
            };
        }
    }
}
