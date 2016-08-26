using Machine.Specifications;
using PowerPuff.Common;
using PowerPuff.Common.Events;
using Prism.Events;
using Prism.Regions;
using SUT = PowerPuff;
using M = Moq;

namespace PowerPuff.Tests.ShellViewModel
{
    [Subject(typeof(SUT.ShellViewModel))]
    public class When_received_event_to_navigate_main_content
    {
        private class FakeView
        {
        }

        private static M.Mock<IRegionManager> _regionManagerMock;
        private static EventAggregator _eventAggregator;
        private static SUT.ShellViewModel _subject;

        Establish context = () =>
        {
            _regionManagerMock = new M.Mock<IRegionManager>();
            _eventAggregator = new EventAggregator();
            _subject = new SUT.ShellViewModel(_regionManagerMock.Object, _eventAggregator);
        };

        Because of = () => _eventAggregator.PublishMainContentNavigationEvent(typeof(FakeView));

        It should_change_content_of_main_region_to_destination_view =
            () =>
                _regionManagerMock.Verify(
                    manager => manager.RequestNavigate(RegionNames.MainContentRegion, typeof(FakeView).FullName));
    }
}