using System;
using Machine.Specifications;
using Moq;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Features.Timer.ViewModels;
using Prism.Regions;
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
                _regionManagerMock = new Mock<IRegionManager>();
                _subject = new TimerMainButtonViewModel(_regionManagerMock.Object);
            };

            Because of = () => _subject.GoToTimerPageCommand.Execute();

            private It should_request_to_go_to_the_timer_page = () =>
                _regionManagerMock.Verify(
                    m => m.RequestNavigate(RegionNames.MainContentRegion, NavigableViews.Main.FeatureLayoutView.GetFullName(), Moq.It.IsAny<Action<NavigationResult>>()));

            private static TimerMainButtonViewModel _subject;
            private static Mock<IRegionManager> _regionManagerMock;
        }
    }
}
