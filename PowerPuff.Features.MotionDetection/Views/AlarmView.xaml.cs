using PowerPuff.Features.MotionDetection.ViewModels;
using System.Windows.Controls;

namespace PowerPuff.Features.MotionDetection.Views
{
    public partial class AlarmView : UserControl
    {
        public AlarmView(AlarmViewModel alarmViewModel)
        {
            InitializeComponent();
            DataContext = alarmViewModel;
        }
    }
}
