using System.Windows.Controls;
using PowerPuff.ViewModels;

namespace PowerPuff.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView(SettingsViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
