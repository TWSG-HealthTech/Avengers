using Machine.Specifications;
using PowerPuff.Common.Events;
using Prism.Events;
using SUT = PowerPuff.Features.Timer.ViewModels;
using M = Moq;

namespace PowerPuff.Features.Timer.Tests.ViewModels.TimerMainViewModel
{
    [Subject(typeof(SUT.TimerMainViewModel))]
    class When_back_button_in_timer_is_clicked
    {
        Establish context = () =>
        {
            _eventMock = new M.Mock<HomeNavigationEvent>();
            var eventAggregatorMock = new M.Mock<IEventAggregator>();
            eventAggregatorMock.Setup(aggregator => aggregator.GetEvent<HomeNavigationEvent>())
                .Returns(_eventMock.Object);
            _subject = new SUT.TimerMainViewModel(eventAggregatorMock.Object);
        };

        Because of = () => _subject.GoBackCommand.Execute();

        private It should_request_to_go_back_to_home_view = () => _eventMock.Verify(e => e.Publish());

        private static SUT.TimerMainViewModel _subject;
        private static M.Mock<HomeNavigationEvent> _eventMock;
    }
}
