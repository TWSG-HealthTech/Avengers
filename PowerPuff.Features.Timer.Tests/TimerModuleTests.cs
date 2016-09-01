using Autofac;
using Machine.Specifications;
using Moq;
using PowerPuff.Common;
using PowerPuff.Features.Timer.Views;
using Prism.Regions;
using It = Machine.Specifications.It;

namespace PowerPuff.Features.Timer.Tests
{
    public class TimerModuleTests
    {
        [Subject(typeof(TimerModule))]
        public class When_initialize_timer_module
        {
            private static TimerModule _subject;
            private static Mock<IRegionManager> _regionManagerMock;
            private static IContainer _container;

            Establish context = () =>
            {
                _regionManagerMock = new Mock<IRegionManager>();
                var containerBuilder = new ContainerBuilder();
                _container = containerBuilder.Build();
                _subject = new TimerModule(_regionManagerMock.Object, _container);
            };

            Because of = () => _subject.Initialize();

            It should_register_view_with_main_buttons_region = () =>
                _regionManagerMock.Verify(manager => manager.RegisterViewWithRegion(RegionNames.MainButtonsRegion, typeof(TimerMainButtonView)));
        }
    }
}
