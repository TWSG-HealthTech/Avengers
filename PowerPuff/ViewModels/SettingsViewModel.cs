using PowerPuff.Common.Events;
using Prism.Commands;
using Prism.Events;

namespace PowerPuff.ViewModels
{
    public class SettingsViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public SettingsViewModel(IEventAggregator eventAggregator)
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
