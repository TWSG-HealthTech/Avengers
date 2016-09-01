using System.Collections.ObjectModel;
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

        public ProfileSettingsViewModel(IProfileGateway profileGateway)
        {
            _profileGateway = profileGateway;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Profile = _profileGateway.GetProfileBy("a111222a").Result;
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
