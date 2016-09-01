using System.Windows.Controls;

namespace PowerPuff.Settings
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
