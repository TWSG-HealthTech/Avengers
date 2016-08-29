using System;
using System.Collections.Generic;
using Machine.Specifications;
using PowerPuff.Common.Settings;
using Prism.Regions;
using SUT = PowerPuff.ViewModels;
using M = Moq;

namespace PowerPuff.Tests.ViewModels.SettingsViewModel
{
    [Subject(typeof(SUT.SettingsViewModel))]
    public class When_navigated_to_settings_view_model
    {
        private Establish context = () =>
        {
            _settingsRepository = new M.Mock<ISettingsRepository>();
            _settingsRepository.Setup(r => r.FindAll()).Returns(new List<SettingMenuViewModel>
            {
                new SettingMenuViewModel("some title", "someViewId")
            });
            _subject = new SUT.SettingsViewModel(_settingsRepository.Object);
        };

        private Because of = NavigateTo;

        private It should_find_all_menu_settings = () => _settingsRepository.Verify(r => r.FindAll());

        private It should_load_all_menu_settings = () => _subject.SettingMenus.ShouldContain(new SettingMenuViewModel("some title", "someViewId"));

        private static void NavigateTo()
        {
            var regionNavigationServiceMock = new M.Mock<IRegionNavigationService>();
            var context = new NavigationContext(regionNavigationServiceMock.Object, new Uri("some uri", UriKind.RelativeOrAbsolute));
            _subject.OnNavigatedTo(context);
        }

        private static M.Mock<ISettingsRepository> _settingsRepository;
        private static SUT.SettingsViewModel _subject;
    }
}
