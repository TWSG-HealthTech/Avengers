using System;
using Machine.Specifications;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using Prism.Regions;
using SUT = PowerPuff.Features.Timer.ViewModels;
using M = Moq;

namespace PowerPuff.Features.Timer.Tests.ViewModels.TimerMainButtonViewModel
{
    [Subject(typeof(SUT.TimerMainButtonViewModel))]
    class When_timer_button_is_clicked
    {
        Establish context = () =>
        {
            _regionManagerMock = new M.Mock<IRegionManager>();
            _subject = new SUT.TimerMainButtonViewModel(_regionManagerMock.Object);
        };

        Because of = () => _subject.GoToTimerPageCommand.Execute();

        private It should_request_to_go_to_the_timer_page = () =>
            _regionManagerMock.Verify(
                m => m.RequestNavigate(RegionNames.MainContentRegion, NavigableViews.Main.FeatureLayoutView.GetFullName(), M.It.IsAny<Action<NavigationResult>>()));

        private static SUT.TimerMainButtonViewModel _subject;
        private static M.Mock<IRegionManager> _regionManagerMock;
    }
}