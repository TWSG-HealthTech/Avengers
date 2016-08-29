using System.Collections.Generic;

namespace PowerPuff.Common.Settings
{
    public interface ISettingsRepository
    {
        void RegisterMenu(string title, string settingContentViewId);
        IEnumerable<SettingMenuViewModel> FindAll();
    }
}
