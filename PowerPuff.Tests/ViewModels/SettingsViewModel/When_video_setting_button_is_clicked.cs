using Machine.Specifications;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Common.Settings;
using Prism.Regions;
using SUT = PowerPuff.ViewModels;
using M = Moq;

namespace PowerPuff.Tests.ViewModels.SettingsViewModel
{
    [Subject(typeof(SUT.SettingsViewModel))]
    public class When_video_setting_button_is_clicked
    {
        private Establish context = () =>
        {
            _settingsRepositoryMock = new M.Mock<ISettingsRepository>();
            _scopedRegionManagerMock = new M.Mock<IRegionManager>();
            _subject = new SUT.SettingsViewModel(_settingsRepositoryMock.Object)
            {
                RegionManager = _scopedRegionManagerMock.Object,
                SelectedMenu = new SettingMenuViewModel("some menu title", "someViewId")
            };
        };

        private Because of = () => _subject.LoadSettingsViewCommand.Execute();

        private It should_request_to_load_video_settings_view =
            () =>
                _scopedRegionManagerMock.Verify(m => m.RequestNavigate(RegionNames.SettingContentRegion,
                    "someViewId"));

        private static M.Mock<ISettingsRepository> _settingsRepositoryMock;
        private static SUT.SettingsViewModel _subject;
        private static M.Mock<IRegionManager> _scopedRegionManagerMock;
    }
}
