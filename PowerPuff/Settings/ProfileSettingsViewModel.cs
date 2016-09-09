using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace PowerPuff.Settings
{
    public class ProfileSettingsViewModel : BindableBase, INavigationAware
    {
        private readonly IProfileGateway _profileGateway;

        private Profile _profile;
        public Profile Profile
        {
            get { return _profile; }
            set { SetProperty(ref _profile, value); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private SocialConnection _selectedConnection;
        public SocialConnection SelectedConnection
        {
            get { return _selectedConnection; }
            set { SetProperty(ref _selectedConnection, value); }
        }

        public ProfileSettingsViewModel(IProfileGateway profileGateway)
        {
            _profileGateway = profileGateway;

            UpdateConnectionCommand = DelegateCommand.FromAsyncHandler(UpdateConnection);
        }

        public DelegateCommand UpdateConnectionCommand { get; private set; }
        private async Task UpdateConnection()
        {
            if (SelectedConnection.IsValid())
            {
                await _profileGateway.UpdateConnection("a111222a", SelectedConnection);

                Message = "Connection Updated";
            }
            else
            {
                Message = "";
            }
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            IsLoading = true;

            Profile = await _profileGateway.GetProfileBy("a111222a");

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
