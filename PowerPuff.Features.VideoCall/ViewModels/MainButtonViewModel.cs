using PowerPuff.Common.Events;
using PowerPuff.Features.VideoCall.Views;
using Prism.Commands;
using Prism.Events;

namespace PowerPuff.Features.VideoCall.ViewModels
{
    public class MainButtonViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public MainButtonViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            GoToVideoPageCommand = new DelegateCommand(GoToVideoPage);
        }

        public DelegateCommand GoToVideoPageCommand { get; private set; }

        private void GoToVideoPage()
        {
            _eventAggregator.GetEvent<NavigationEvent>().Publish(typeof(VideoMainView));
        }
    }
}
