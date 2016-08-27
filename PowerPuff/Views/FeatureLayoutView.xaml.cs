using System.Windows.Controls;
using PowerPuff.ViewModels;

namespace PowerPuff.Views
{
    /// <summary>
    /// Interaction logic for FeatureLayoutView.xaml
    /// </summary>
    public partial class FeatureLayoutView : UserControl
    {
        public FeatureLayoutView(FeatureLayoutViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
