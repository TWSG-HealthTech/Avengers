using PowerPuff.Common.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace PowerPuff.Features.VideoCall.ViewModels
{
    public class VideoMainViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public VideoMainViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            GoBackCommand = new DelegateCommand(GoBack);
        }

        public DelegateCommand GoBackCommand { get; private set; }

        private void GoBack()
        {
            _eventAggregator.GetEvent<HomeNavigationEvent>().Publish();
        }
    }
}
