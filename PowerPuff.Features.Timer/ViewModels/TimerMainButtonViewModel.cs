using PowerPuff.Features.Timer.Navigation;
using Prism.Commands;

namespace PowerPuff.Features.Timer.ViewModels
{
    public class TimerMainButtonViewModel
    {
        public TimerMainButtonViewModel(TimerNavigator timerNavigator)
        {
            GoToTimerPageCommand = new DelegateCommand(timerNavigator.GoToTimerPage);
        }

        public DelegateCommand GoToTimerPageCommand { get; private set; }
    }
}
