using Prism.Regions;

namespace PowerPuff.Features.VideoCall.ViewModels
{
    public class SettingsViewModel : INavigationAware
    {
        private readonly IGateway _gateway;

        public SettingsViewModel(IGateway gateway)
        {
            _gateway = gateway;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
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
