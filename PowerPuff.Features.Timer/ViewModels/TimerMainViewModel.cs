using PowerPuff.Common.Events;
using Prism.Commands;
using Prism.Events;

namespace PowerPuff.Features.Timer.ViewModels
{
    public class TimerMainViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public TimerMainViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            GoBackCommand = new DelegateCommand(GoBack);
        }

        public DelegateCommand GoBackCommand { get; set; }

        private void GoBack()
        {
            _eventAggregator.GetEvent<HomeNavigationEvent>().Publish();
        }
    }
}
