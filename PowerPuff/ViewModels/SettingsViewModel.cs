using System.Collections.ObjectModel;
using PowerPuff.Common;
using PowerPuff.Common.Prism;
using PowerPuff.Common.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace PowerPuff.ViewModels
{
    public class SettingsViewModel : BindableBase, INavigationAware, IRegionManagerAware
    {
        private readonly ISettingsRepository _settingsRepository;
        public ObservableCollection<SettingMenuViewModel> SettingMenus { get; private set; }

        private SettingMenuViewModel _selectedMenu;
        public SettingMenuViewModel SelectedMenu
        {
            get { return _selectedMenu; }
            set { SetProperty(ref _selectedMenu, value); }
        }

        public SettingsViewModel(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;

            SettingMenus = new ObservableCollection<SettingMenuViewModel>();

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
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public IRegionManager RegionManager { get; set; }
    }
}
