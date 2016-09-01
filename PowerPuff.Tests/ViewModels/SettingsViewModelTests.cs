using System;
using System.Collections.Generic;
using Machine.Specifications;
using Moq;
using PowerPuff.Common;
using PowerPuff.Common.Settings;
using PowerPuff.Settings;
using Prism.Regions;
using It = Machine.Specifications.It;

namespace PowerPuff.Tests.ViewModels
{
    [Subject(typeof(SettingsViewModel))]
    public class SettingsViewModelTests
    {
        private static Mock<IMenuSettingsRepository> _settingsRepositoryMock;
        private static SettingsViewModel _subject;
        private static Mock<IRegionManager> _scopedRegionManagerMock;

        private Establish context = () =>
        {
            _settingsRepositoryMock = new Mock<IMenuSettingsRepository>();
            _scopedRegionManagerMock = new Mock<IRegionManager>();
            _subject = new SettingsViewModel(_settingsRepositoryMock.Object)
            {
                RegionManager = _scopedRegionManagerMock.Object,
                SelectedMenu = new MenuSettingViewModel("some menu title", "someViewId")
            };
        };

        public class When_navigated_to_settings_view_model
        {
            private Establish context = () =>
            {
                _settingsRepositoryMock.Setup(r => r.FindAll()).Returns(new List<MenuSettingViewModel>
                {
                    new MenuSettingViewModel("some title", "someViewId")
                });
            };

            private Because of = NavigateTo;

            private It should_find_all_menu_settings = () => _settingsRepositoryMock.Verify(r => r.FindAll());

            private It should_load_all_menu_settings = () => _subject.SettingMenus.ShouldContain(new MenuSettingViewModel("some title", "someViewId"));

            private static void NavigateTo()
            {
                var regionNavigationServiceMock = new Mock<IRegionNavigationService>();
                var context = new NavigationContext(regionNavigationServiceMock.Object, new Uri("some uri", UriKind.RelativeOrAbsolute));
                _subject.OnNavigatedTo(context);
            }
        }
        
        public class When_video_setting_button_is_clicked
        {
            private Because of = () => _subject.LoadSettingsViewCommand.Execute();

            private It should_request_to_load_video_settings_view =
                () =>
                    _scopedRegionManagerMock.Verify(m => m.RequestNavigate(RegionNames.SettingContentRegion,
                        "someViewId"));

            
        }
    }
}
