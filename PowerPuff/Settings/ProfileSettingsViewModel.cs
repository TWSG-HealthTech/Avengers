using System.Collections.ObjectModel;
using Prism.Regions;

namespace PowerPuff.Settings
{
    public class ProfileSettingsViewModel : INavigationAware
    {
        private readonly IProfileGateway _profileGateway;

        public ObservableCollection<SocialConnection> Connections { get; set; }

        public ProfileSettingsViewModel(IProfileGateway profileGateway)
        {
            _profileGateway = profileGateway;

            Connections = new ObservableCollection<SocialConnection>();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var connections = _profileGateway.GetAllSocialConnections("a111222a").Result;
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
