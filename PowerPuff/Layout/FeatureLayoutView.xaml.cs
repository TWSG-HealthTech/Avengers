using System.Windows.Controls;

namespace PowerPuff.Layout
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
