using System.Collections.ObjectModel;
using PowerPuff.Features.VideoCall.Models;
using Prism.Regions;

namespace PowerPuff.Features.VideoCall.ViewModels
{
    public class SettingsViewModel : INavigationAware
    {
        private readonly IGateway _gateway;

        public ObservableCollection<SocialConnection> Connections { get; set; }

        public SettingsViewModel(IGateway gateway)
        {
            _gateway = gateway;

            Connections = new ObservableCollection<SocialConnection>();
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            var connections = await _gateway.GetSocialConnections("a111222a");

            Connections.AddRange(connections);
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