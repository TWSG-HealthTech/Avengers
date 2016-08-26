using Machine.Specifications;
using PowerPuff.Common.Events;
using Prism.Events;
using SUT = PowerPuff.Features.VideoCall.ViewModels;
using M = Moq;

namespace PowerPuff.Features.VideoCall.Tests.ViewModels.VideoMainViewModel
{
    [Subject(typeof(SUT.VideoMainViewModel))]
    public class When_back_button_is_clicked
    {
        Establish context = () =>
        {
            _eventMock = new M.Mock<HomeNavigationEvent>();
            var eventAggregator = new M.Mock<IEventAggregator>();
            eventAggregator.Setup(aggregator => aggregator.GetEvent<HomeNavigationEvent>())
                .Returns(_eventMock.Object);
            _subject = new SUT.VideoMainViewModel(eventAggregator.Object);
        };

        Because of = () => _subject.GoBackCommand.Execute();

        private It should_request_to_go_back_to_home_view = () => _eventMock.Verify(e => e.Publish());

        private static SUT.VideoMainViewModel _subject;
        private static M.Mock<HomeNavigationEvent> _eventMock;
    }
}
