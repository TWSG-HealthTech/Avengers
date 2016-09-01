using System.Collections.ObjectModel;
using PowerPuff.Common;
using PowerPuff.Common.Prism;
using PowerPuff.Common.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace PowerPuff.Settings
{
    public class SettingsViewModel : BindableBase, INavigationAware, IRegionManagerAware
    {
        private readonly IMenuSettingsRepository _settingsRepository;
        public ObservableCollection<MenuSettingViewModel> SettingMenus { get; private set; }

        private MenuSettingViewModel _selectedMenu;
        public MenuSettingViewModel SelectedMenu
        {
            get { return _selectedMenu; }
            set { SetProperty(ref _selectedMenu, value); }
        }

        public SettingsViewModel(IMenuSettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;

            SettingMenus = new ObservableCollection<MenuSettingViewModel>();

            LoadSettingsViewCommand = new DelegateCommand(LoadSettingsView);
        }

        public DelegateCommand LoadSettingsViewCommand { get; private set; }

        private void LoadSettingsView()
        {
            if (SelectedMenu != null)
            {
                RegionManager.RequestNavigate(RegionNames.SettingContentRegion, SelectedMenu.SettingContentViewId);   
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            SettingMenus.Clear();

            SettingMenus.AddRange(_settingsRepository.FindAll());
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public IRegionManager RegionManager { get; set; }
    }
}
