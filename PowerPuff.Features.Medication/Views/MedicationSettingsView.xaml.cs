using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
}
