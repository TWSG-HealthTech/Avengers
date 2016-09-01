using Autofac;
using Machine.Specifications;
using Moq;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Common.Settings;
using PowerPuff.Features.VideoCall.Views;
using Prism.Regions;
using It = Machine.Specifications.It;

namespace PowerPuff.Features.VideoCall.Tests
{
    public class VideoCallModuleTests
    {
        [Subject(typeof(VideoCallModule))]
        public class When_initialize_module
        {
            private static VideoCallModule _subject;
            private static Mock<IRegionManager> _regionManagerMock;
            private static IContainer _container;
            private static Mock<IMenuSettingsRepository> _settingsRepositoryMock;

            Establish context = () =>
            {
                _regionManagerMock = new Mock<IRegionManager>();
                var containerBuilder = new ContainerBuilder();
                _settingsRepositoryMock = new Mock<IMenuSettingsRepository>();
                containerBuilder.Register(context => _settingsRepositoryMock.Object);
                _container = containerBuilder.Build();
                _subject = new VideoCallModule(_regionManagerMock.Object, _container);
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
}
