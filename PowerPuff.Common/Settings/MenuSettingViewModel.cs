using System;

namespace PowerPuff.Common.Settings
{
    public class MenuSettingViewModel : IEquatable<MenuSettingViewModel>
    {
        public string Title { get; private set; }
        public string SettingContentViewId { get; private set; }

        public MenuSettingViewModel(string title, string settingContentViewId)
        {
            Title = title;
            SettingContentViewId = settingContentViewId;
        }

        public bool Equals(MenuSettingViewModel other)
        {
            return Title == other.Title && 
                SettingContentViewId == other.SettingContentViewId;
        }

        public override bool Equals(object obj)
        {
            var other = obj as MenuSettingViewModel;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash*23 + Title?.GetHashCode() ?? 0;
                hash = hash*23 + SettingContentViewId?.GetHashCode() ?? 0;
                return hash;
            }
        }
    }
}
