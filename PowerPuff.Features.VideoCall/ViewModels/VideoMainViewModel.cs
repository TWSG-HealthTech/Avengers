using System.Collections.ObjectModel;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Features.VideoCall.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace PowerPuff.Features.VideoCall.ViewModels
{
    public class VideoMainViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IVideoCallService _videoCallService;

        public ObservableCollection<SocialConnection> SocialConnections { get; set; }


        private SocialConnection _selectedConnection;
        public SocialConnection SelectedConnection
        {
            get { return _selectedConnection; }
            set { SetProperty(ref _selectedConnection, value); }
        }

        public VideoMainViewModel(IRegionManager regionManager, IVideoCallService videoCallService)
        {
            _regionManager = regionManager;
            _videoCallService = videoCallService;

            SocialConnections = new ObservableCollection<SocialConnection>();

            GoBackCommand = new DelegateCommand(GoBack);
            CallCommand = new DelegateCommand(Call);
        }

        public DelegateCommand GoBackCommand { get; private set; }

        private void GoBack()
        {
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, NavigableViews.Main.HomeView.GetFullName());
        }

        public DelegateCommand CallCommand { get; private set; }

        private void Call()
        {
            _videoCallService.Call(SelectedConnection, () => {});
        }
    }
}
