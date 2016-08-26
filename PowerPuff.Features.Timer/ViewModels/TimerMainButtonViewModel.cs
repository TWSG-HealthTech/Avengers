using PowerPuff.Common.Events;
using PowerPuff.Features.Timer.Views;
using Prism.Commands;
using Prism.Events;

namespace PowerPuff.Features.Timer.ViewModels
{
    public class TimerMainButtonViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public TimerMainButtonViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            GoToTimerPageCommand = new DelegateCommand(GoToTimerPage);
        }

        public DelegateCommand GoToTimerPageCommand { get; private set; }

        private void GoToTimerPage()
        {
            _eventAggregator.PublishMainContentNavigationEvent(typeof(TimerMainView));
        }
    }
}
