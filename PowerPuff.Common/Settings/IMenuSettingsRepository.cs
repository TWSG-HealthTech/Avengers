using System.Collections.Generic;

namespace PowerPuff.Common.Settings
{
    public interface IMenuSettingsRepository
    {
        void RegisterMenu(string title, string settingContentViewId);
        IEnumerable<MenuSettingViewModel> FindAll();
    }
}
