using Machine.Specifications;
using PowerPuff.Common;
using PowerPuff.Common.Events;
using PowerPuff.Features.Timer.Views;
using Prism.Events;
using SUT = PowerPuff.Features.Timer.ViewModels;
using M = Moq;

namespace PowerPuff.Features.Timer.Tests.ViewModels.TimerMainButtonViewModel
{
    [Subject(typeof(SUT.TimerMainButtonViewModel))]
    class When_timer_button_is_clicked
    {
        Establish context = () =>
        {
            _eventMock = new M.Mock<NavigationEvent>();
            var eventAggregator = new M.Mock<IEventAggregator>();
            eventAggregator.Setup(aggregator => aggregator.GetEvent<NavigationEvent>()).Returns(_eventMock.Object);
            _subject = new SUT.TimerMainButtonViewModel(eventAggregator.Object);
        };

        Because of = () => _subject.GoToTimerPageCommand.Execute();

        It should_request_to_go_to_the_timer_page = () =>
            _eventMock.Verify(
                e => e.Publish(new NavigationEventPayload(RegionNames.MainContentRegion,
                    typeof(TimerMainView))));

        private static SUT.TimerMainButtonViewModel _subject;
        private static M.Mock<NavigationEvent> _eventMock;
    }
}
