using System.Collections.ObjectModel;
using PowerPuff.Features.VideoCall.Models;
using Prism.Mvvm;
using Prism.Regions;

namespace PowerPuff.Features.VideoCall.ViewModels
{
    public class SettingsViewModel : BindableBase, INavigationAware
    {
        private readonly IGateway _gateway;

        public ObservableCollection<SocialConnection> Connections { get; set; }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        public SettingsViewModel(IGateway gateway)
        {
            _gateway = gateway;

            Connections = new ObservableCollection<SocialConnection>();
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