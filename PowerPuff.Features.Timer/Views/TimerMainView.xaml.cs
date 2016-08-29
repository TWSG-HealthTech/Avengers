using System.Windows.Controls;
using PowerPuff.Features.Timer.ViewModels;
using System.Windows.Threading;
using System;

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
