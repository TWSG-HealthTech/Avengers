using Machine.Specifications;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using Prism.Regions;
using SUT = PowerPuff.Features.VideoCall.ViewModels;
using M = Moq;

namespace PowerPuff.Features.VideoCall.Tests.ViewModels.VideoMainViewModel
{
    [Subject(typeof(SUT.VideoMainViewModel))]
    public class When_back_button_is_clicked
    {
        Establish context = () =>
        {
            _regionManagerMock = new M.Mock<IRegionManager>();
            var videoCallServiceMock = new M.Mock<SUT.IVideoCallService>();
            _subject = new SUT.VideoMainViewModel(_regionManagerMock.Object, videoCallServiceMock.Object);
        };

        Because of = () => _subject.GoBackCommand.Execute();

        private It should_request_to_go_back_to_home_view =
            () =>
                _regionManagerMock.Verify(
                    m => m.RequestNavigate(RegionNames.MainContentRegion, NavigableViews.Main.HomeView.GetFullName()));

        private static SUT.VideoMainViewModel _subject;
        private static M.Mock<IRegionManager> _regionManagerMock;
    }
}
