using PowerPuff.Features.Timer.ViewModels;

namespace PowerPuff.Features.Timer.Views
{
    /// <summary>
    /// Interaction logic for MainButtonView.xaml
    /// </summary>
    public partial class TimerMainButtonView
    {
        public TimerMainButtonView(TimerMainButtonViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
