using System.Windows.Controls;
using PowerPuff.ViewModels;

namespace PowerPuff.Views
{
    /// <summary>
    /// Interaction logic for SocialConnectionSettingsView.xaml
    /// </summary>
    public partial class SocialConnectionSettingsView : UserControl
    {
        public SocialConnectionSettingsView(SocialConnectionSettingsViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
