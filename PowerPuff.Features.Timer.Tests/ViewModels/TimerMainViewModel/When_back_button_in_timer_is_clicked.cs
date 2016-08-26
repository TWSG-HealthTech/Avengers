using Machine.Specifications;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using Prism.Regions;
using SUT = PowerPuff.Features.Timer.ViewModels;
using M = Moq;

namespace PowerPuff.Features.Timer.Tests.ViewModels.TimerMainViewModel
{
    [Subject(typeof(SUT.TimerMainViewModel))]
    class When_back_button_in_timer_is_clicked
    {
        Establish context = () =>
        {
            _regionManagerMock = new M.Mock<IRegionManager>();
            _subject = new SUT.TimerMainViewModel(_regionManagerMock.Object);
        };

        Because of = () => _subject.GoBackCommand.Execute();

        private It should_request_to_go_back_to_home_view = () => _regionManagerMock.Verify(
            m => m.RequestNavigate(RegionNames.MainContentRegion, NavigableViews.Main.HomeView.GetFullName()));

        private static SUT.TimerMainViewModel _subject;
        private static M.Mock<IRegionManager> _regionManagerMock;
    }
}
