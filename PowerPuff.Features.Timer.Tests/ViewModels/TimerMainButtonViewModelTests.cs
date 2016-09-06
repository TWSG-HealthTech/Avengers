using Machine.Specifications;
using Moq;
using PowerPuff.Features.Timer.Navigation;
using PowerPuff.Features.Timer.Tests.Navigation;
using PowerPuff.Features.Timer.ViewModels;
using PowerPuff.Test.Helpers;
using Prism.Regions;

namespace PowerPuff.Features.Timer.Tests.ViewModels
{
    public class TimerMainButtonViewModelTests
    {
        [Subject(typeof(TimerMainButtonViewModel))]
        class When_timer_button_is_clicked
        {
            Establish context = () =>
            {
                _regionManagerMock = new Mock<IRegionManager>();
                var timerNavigator = new TimerNavigator(_regionManagerMock.Object, new TestDispatcher());
                _subject = new TimerMainButtonViewModel(timerNavigator);
            };

            Because of = () => _subject.GoToTimerPageCommand.Execute();

            Behaves_like<TimerNavigatorBehaviors> it_navigates_to_timer_view;
            
            protected static TimerMainButtonViewModel _subject;
            protected static Mock<IRegionManager> _regionManagerMock;
        }
    }
}
