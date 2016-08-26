using System.Windows.Controls;
using PowerPuff.Features.Timer.ViewModels;


namespace PowerPuff.Features.Timer.Views
{
    /// <summary>
    /// Interaction logic for TimerMainView.xaml
    /// </summary>
    public partial class TimerMainView : UserControl
    {
        public TimerMainView(TimerMainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
