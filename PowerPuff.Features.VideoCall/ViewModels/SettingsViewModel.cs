using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PowerPuff.Features.VideoCall.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace PowerPuff.Features.VideoCall.ViewModels
{
    public class SettingsViewModel : BindableBase, INavigationAware
    {
        private readonly IGateway _gateway;

        public ObservableCollection<SocialConnection> Connections { get; set; }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        private SocialConnection _selectedConnection;
        public SocialConnection SelectedConnection
        {
            get { return _selectedConnection; }
            set { SetProperty(ref _selectedConnection, value); }
        }

        public SettingsViewModel(IGateway gateway)
        {
            _gateway = gateway;

            Connections = new ObservableCollection<SocialConnection>();

            UpdateSkypeCommand = DelegateCommand.FromAsyncHandler(UpdateSkype);
        }

        public DelegateCommand UpdateSkypeCommand { get; private set; }
        private async Task UpdateSkype()
        {
            Message = "";

            await _gateway.Update("a111222a", SelectedConnection);

            Message = "Connection updated";
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            IsLoading = true;

            var connections = await _gateway.GetSocialConnections("a111222a");

            Connections.AddRange(connections);

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