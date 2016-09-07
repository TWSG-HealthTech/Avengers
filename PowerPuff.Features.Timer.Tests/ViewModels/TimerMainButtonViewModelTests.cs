using Machine.Specifications;
using Moq;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Common.Navigation;
using PowerPuff.Features.Timer.ViewModels;
using It = Machine.Specifications.It;

namespace PowerPuff.Features.Timer.Tests.ViewModels
{
    public class TimerMainButtonViewModelTests
    {
        [Subject(typeof(TimerMainButtonViewModel))]
        class When_timer_button_is_clicked
        {
            Establish context = () =>
            {
                _navigatorMock = new Mock<INavigator>();
                _subject = new TimerMainButtonViewModel(_navigatorMock.Object);
            };

            Because of = () => _subject.GoToTimerPageCommand.Execute();

            It navigates_to_timer_view = () => _navigatorMock.Verify(n => n.GoToPage(NavigableViews.Timer.MainView.GetFullName()));


            protected static TimerMainButtonViewModel _subject;
            protected static Mock<INavigator> _navigatorMock;
        }
    }
}
