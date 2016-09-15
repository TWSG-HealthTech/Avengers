using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace PowerPuff.Features.Medication.Views
{
    /// <summary>
    /// Interaction logic for MedicationMainButtonView.xaml
    /// </summary>
    public partial class MedicationMainButtonView
    {
        public MedicationMainButtonView(MedicationMainButtonViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
