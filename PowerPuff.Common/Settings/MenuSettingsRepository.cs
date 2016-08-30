using System;
using System.Collections.Generic;

namespace PowerPuff.Common.Settings
{
    public class MenuSettingsRepository : IMenuSettingsRepository
    {
        private readonly Dictionary<string, MenuSettingViewModel> _settings;

        public MenuSettingsRepository()
        {
            _settings = new Dictionary<string, MenuSettingViewModel>();    
        }

        public void RegisterMenu(string title, string settingContentViewId)
        {
            if(_settings.ContainsKey(title)) throw new ArgumentException($"Menu with title '{title}' exists");

            _settings[title] = new MenuSettingViewModel(title, settingContentViewId);
        }

        public IEnumerable<MenuSettingViewModel> FindAll()
        {
            return _settings.Values;
        }
    }
}
