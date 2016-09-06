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

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        public ProfileSettingsViewModel(IProfileGateway profileGateway)
        {
            _profileGateway = profileGateway;
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
