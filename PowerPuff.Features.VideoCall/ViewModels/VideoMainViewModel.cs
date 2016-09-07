using System.Collections.ObjectModel;
using PowerPuff.Features.VideoCall.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace PowerPuff.Features.VideoCall.ViewModels
{
    public class VideoMainViewModel : BindableBase, INavigationAware
    {
        private readonly IVideoCallService _videoCallService;
        private readonly IGateway _gateway;

        public ObservableCollection<SocialConnection> SocialConnections { get; set; }


        private SocialConnection _selectedConnection;
        public SocialConnection SelectedConnection
        {
            get { return _selectedConnection; }
            set { SetProperty(ref _selectedConnection, value); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        public VideoMainViewModel(IVideoCallService videoCallService, IGateway gateway)
        {
            _videoCallService = videoCallService;
            _gateway = gateway;

            SocialConnections = new ObservableCollection<SocialConnection>();
                        
            CallCommand = new DelegateCommand(Call);
        }

        public DelegateCommand CallCommand { get; private set; }

        private void Call()
        {
            _videoCallService.Call(SelectedConnection, () => {});
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            IsLoading = true;

            var connections = await _gateway.GetSocialConnections("a111222a");

            SocialConnections.AddRange(connections);

            IsLoading = false;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
