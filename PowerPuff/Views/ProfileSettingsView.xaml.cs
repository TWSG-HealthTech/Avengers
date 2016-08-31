using System.Windows.Controls;
using PowerPuff.ViewModels;

namespace PowerPuff.Views
{
    /// <summary>
    /// Interaction logic for ProfileSettingsView.xaml
    /// </summary>
    public partial class ProfileSettingsView : UserControl
    {
        public ProfileSettingsView(ProfileSettingsViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
