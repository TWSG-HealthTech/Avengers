using System.Collections.ObjectModel;
using PowerPuff.Features.VideoCall.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace PowerPuff.Features.VideoCall.ViewModels
{
    public class VideoMainViewModel : BindableBase
    {
        private readonly IVideoCallService _videoCallService;

        public ObservableCollection<SocialConnection> SocialConnections { get; set; }


        private SocialConnection _selectedConnection;
        public SocialConnection SelectedConnection
        {
            get { return _selectedConnection; }
            set { SetProperty(ref _selectedConnection, value); }
        }

        public VideoMainViewModel(IVideoCallService videoCallService)
        {
            _videoCallService = videoCallService;

            SocialConnections = new ObservableCollection<SocialConnection>();
                        
            CallCommand = new DelegateCommand(Call);
        }

        public DelegateCommand CallCommand { get; private set; }

        private void Call()
        {
            _videoCallService.Call(SelectedConnection, () => {});
        }
    }
}
