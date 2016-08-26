using Machine.Specifications;
using Prism.Events;
using SUT = PowerPuff.Common.Events;
using M = Moq;

namespace PowerPuff.Common.Tests.Events.EventAggregatorExtensions
{
    [Subject(typeof(SUT.EventAggregatorExtensions))]
    public class When_invoke_publish_main_content_event
    {
        private class FakeView { }
        private static M.Mock<IEventAggregator> _eventAggregatorMock;
        private static M.Mock<SUT.NavigationEvent> _eventMock;

        private Establish context = () =>
        {
            _eventMock = new M.Mock<SUT.NavigationEvent>();
            _eventAggregatorMock = new M.Mock<IEventAggregator>();
            _eventAggregatorMock.Setup(aggregator => aggregator.GetEvent<SUT.NavigationEvent>())
                .Returns(_eventMock.Object);
        };

        private Because of =
            () =>
                SUT.EventAggregatorExtensions.PublishMainContentNavigationEvent(_eventAggregatorMock.Object,
                    typeof(FakeView));

        It should_produce_event_with_destination_region_is_main_content =
            () =>
                _eventMock.Verify(
                    e => e.Publish(new SUT.NavigationEventPayload(RegionNames.MainContentRegion, typeof(FakeView))));
    }
}
