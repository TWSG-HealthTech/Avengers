using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Features.Timer.Navigation;
using Prism.Commands;

namespace PowerPuff.Features.Timer.ViewModels
{
    public class TimerMainButtonViewModel
    {
        public TimerMainButtonViewModel(INavigator navigator)
        {
            GoToTimerPageCommand = new DelegateCommand(() => navigator.GoToPage(NavigableViews.Timer.MainView.GetFullName()));
        }

        public DelegateCommand GoToTimerPageCommand { get; private set; }
    }
}
