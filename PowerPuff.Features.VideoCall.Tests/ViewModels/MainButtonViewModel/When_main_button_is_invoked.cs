using System;
using Machine.Specifications;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using Prism.Regions;
using SUT = PowerPuff.Features.VideoCall.ViewModels;
using M = Moq;

namespace PowerPuff.Features.VideoCall.Tests.ViewModels.MainButtonViewModel
{
    [Subject(typeof(SUT.MainButtonViewModel))]
    public class When_main_button_is_invoked
    {
        Establish context = () =>
        {
            _regionManagerMock = new M.Mock<IRegionManager>();
            _subject = new SUT.MainButtonViewModel(_regionManagerMock.Object);
        };

        private Because of = () => _subject.GoToVideoPageCommand.Execute();

        private It should_request_to_go_to_video_main_view =
            () =>
                _regionManagerMock.Verify(
                    m =>
                        m.RequestNavigate(RegionNames.MainContentRegion, NavigableViews.Main.FeatureLayoutView.GetFullName(), M.It.IsAny<Action<NavigationResult>>()));

        private static SUT.MainButtonViewModel _subject;
        private static M.Mock<IRegionManager> _regionManagerMock;
    }
}
