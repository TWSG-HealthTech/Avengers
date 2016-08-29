using Autofac;
using Machine.Specifications;
using M = Moq;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Common.Settings;
using PowerPuff.Features.VideoCall.Views;
using Prism.Regions;
using SUT = PowerPuff.Features.VideoCall;

namespace PowerPuff.Features.VideoCall.Tests.VideoCallModule
{
    [Subject(typeof(SUT.VideoCallModule))]
    public class When_initialize_module
    {
        private static SUT.VideoCallModule _subject;
        private static M.Mock<IRegionManager> _regionManagerMock;
        private static IContainer _container;
        private static M.Mock<ISettingsRepository> _settingsRepositoryMock;

        Establish context = () =>
        {
            _regionManagerMock = new M.Mock<IRegionManager>();
            var containerBuilder = new ContainerBuilder();
            _settingsRepositoryMock = new M.Mock<ISettingsRepository>();
            containerBuilder.Register(context => _settingsRepositoryMock.Object);
            _container = containerBuilder.Build();
            _subject = new SUT.VideoCallModule(_regionManagerMock.Object, _container);
        };

        Because of = () => _subject.Initialize();

        
        It should_register_view_with_main_buttons_region = () =>
            _regionManagerMock.Verify(
                manager => manager.RegisterViewWithRegion(RegionNames.MainButtonsRegion, typeof(MainButtonView)));


        It should_register_menu_settings = () =>
            _settingsRepositoryMock.Verify(
                r => r.RegisterMenu("Video", NavigableViews.VideoCall.SettingsView.GetFullName()));
    }
}
