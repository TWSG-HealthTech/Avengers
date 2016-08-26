using Autofac;
using Machine.Specifications;
using M = Moq;
using PowerPuff.Common;
using PowerPuff.Features.Timer.Views;
using Prism.Regions;
using SUT = PowerPuff.Features.Timer;

namespace PowerPuff.Features.Timer.Tests
{
    [Subject(typeof(SUT.TimerModule))]
    public class When_initialize_timer_module
    {
        private static SUT.TimerModule _subject;
        private static M.Mock<IRegionManager> _regionManagerMock;
        private static IContainer _container;

        Establish context = () =>
        {
            _regionManagerMock = new M.Mock<IRegionManager>();
            var containerBuilder = new ContainerBuilder();
            _container = containerBuilder.Build();
            _subject = new SUT.TimerModule(_regionManagerMock.Object, _container);
        };

        Because of = () => _subject.Initialize();

        It should_register_view_with_main_buttons_region = () =>
            _regionManagerMock.Verify(manager => manager.RegisterViewWithRegion(RegionNames.MainButtonsRegion, typeof(TimerMainButtonView)));
    }
}
