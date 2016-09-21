using System.Collections.Generic;
using PowerPuff.Features.Medication.Models;
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

    public class FakeMedicationReminderViewModel
    {
        public IList<DrugOrder> DrugsToTake { get; set; }

        public FakeMedicationReminderViewModel()
        {
            DrugsToTake = new List<DrugOrder>()
            {
                new DrugOrder() {DrugName = "Drug A", Dose = "2", DoseUnit = "Tbs"},
                new DrugOrder() {DrugName = "Drug B", Dose = "2", DoseUnit = "Tbs"},
                new DrugOrder() {DrugName = "Drug C", Dose = "2", DoseUnit = "Tbs"}
            };
        }
    }
}