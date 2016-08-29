using System;
using System.Collections.Generic;

namespace PowerPuff.Common.Settings
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly Dictionary<string, SettingMenuViewModel> _settings;

        public SettingsRepository()
        {
            _settings = new Dictionary<string, SettingMenuViewModel>();    
        }

        public void RegisterMenu(string title, string settingContentViewId)
        {
            if(_settings.ContainsKey(title)) throw new ArgumentException($"Menu with title '{title}' exists");

            _settings[title] = new SettingMenuViewModel(title, settingContentViewId);
        }

        public IEnumerable<SettingMenuViewModel> FindAll()
        {
            return _settings.Values;
        }
    }
}
