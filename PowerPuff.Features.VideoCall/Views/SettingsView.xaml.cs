using System.Windows.Controls;
using PowerPuff.Features.VideoCall.ViewModels;

namespace PowerPuff.Features.VideoCall.Views
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
