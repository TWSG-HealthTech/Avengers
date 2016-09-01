using System.Windows.Controls;

namespace PowerPuff.Layout
{
    /// <summary>
    /// Interaction logic for MainButtonsView.xaml
    /// </summary>
    public partial class MainButtonsView : UserControl
    {
        public MainButtonsView(MainButtonsViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
