using Machine.Specifications;
using Moq;
using PowerPuff.Common.Speech;
using PowerPuff.Features.Timer.Navigation;
using PowerPuff.Features.Timer.Speech;
using PowerPuff.Features.Timer.Tests.Navigation;
using PowerPuff.Test.Helpers;
using Prism.Regions;

namespace PowerPuff.Features.Timer.Tests.Speech
{
    [Subject(typeof(TimerIntentHandler))]
    class TimerIntentHandlerTests
    {
        private Establish context = () =>
        {
            _regionManagerMock = new Mock<IRegionManager>();
            var timerNavigator = new TimerNavigator(_regionManagerMock.Object, new TestDispatcher());
            _speechSynthesiserMock = new Mock<ISpeechSynthesiser>();
            _subject = new TimerIntentHandler(_speechSynthesiserMock.Object, timerNavigator);
        };

        private Because of = async () => await _subject.SetTimer(null, null);

        Behaves_like<TimerNavigatorBehaviors> it_navigates_to_timer_view;

        protected static TimerIntentHandler _subject;
        protected static Mock<ISpeechSynthesiser> _speechSynthesiserMock;
        protected static Mock<IRegionManager> _regionManagerMock;
    }
}
