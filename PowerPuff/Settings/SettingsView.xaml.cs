using System.Windows.Controls;
using PowerPuff.Common.Prism;

namespace PowerPuff.Settings
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl, ICreateRegionManagerScope
    {
        public SettingsView(SettingsViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }

        public bool CreateRegionManagerScope { get; } = true;
    }
}
