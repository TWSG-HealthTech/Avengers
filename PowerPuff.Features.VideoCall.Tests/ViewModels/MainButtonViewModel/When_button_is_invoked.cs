using Machine.Specifications;
using PowerPuff.Common;
using PowerPuff.Common.Events;
using PowerPuff.Features.VideoCall.Views;
using Prism.Events;
using SUT = PowerPuff.Features.VideoCall.ViewModels;
using M = Moq;

namespace PowerPuff.Features.VideoCall.Tests.ViewModels.MainButtonViewModel
{
    [Subject(typeof(SUT.MainButtonViewModel))]
    public class When_button_is_invoked
    {
        Establish context = () =>
        {
            _eventMock = new M.Mock<NavigationEvent>();
            var eventAggregator = new M.Mock<IEventAggregator>();
            eventAggregator.Setup(aggregator => aggregator.GetEvent<NavigationEvent>()).Returns(_eventMock.Object);
            _subject = new SUT.MainButtonViewModel(eventAggregator.Object);
        };

        private Because of = () => _subject.GoToVideoPageCommand.Execute();

        private It should_request_to_go_to_video_main_view =
            () =>
                _eventMock.Verify(
                    e =>
                        e.Publish(new NavigationEventPayload(RegionNames.MainContentRegion,
                            typeof(VideoMainView))));
        private static SUT.MainButtonViewModel _subject;
        private static M.Mock<NavigationEvent> _eventMock;
    }
}
