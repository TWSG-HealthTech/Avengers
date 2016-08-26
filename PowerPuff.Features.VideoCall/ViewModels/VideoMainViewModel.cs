using System.Collections.ObjectModel;
using PowerPuff.Common.Events;
using PowerPuff.Features.VideoCall.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace PowerPuff.Features.VideoCall.ViewModels
{
    public class VideoMainViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IVideoCallService _videoCallService;

        public ObservableCollection<SocialConnection> SocialConnections { get; set; }


        private SocialConnection _selectedConnection;
        public SocialConnection SelectedConnection
        {
            get { return _selectedConnection; }
            set { SetProperty(ref _selectedConnection, value); }
        }

        public VideoMainViewModel(IEventAggregator eventAggregator, IVideoCallService videoCallService)
        {
            _eventAggregator = eventAggregator;
            _videoCallService = videoCallService;

            SocialConnections = new ObservableCollection<SocialConnection>();

            GoBackCommand = new DelegateCommand(GoBack);
            CallCommand = new DelegateCommand(Call);
        }

        public DelegateCommand GoBackCommand { get; private set; }

        private void GoBack()
        {
            _eventAggregator.GetEvent<HomeNavigationEvent>().Publish();
        }

        public DelegateCommand CallCommand { get; private set; }

        private void Call()
        {
            _videoCallService.Call(SelectedConnection, () => {});
        }
    }
}
