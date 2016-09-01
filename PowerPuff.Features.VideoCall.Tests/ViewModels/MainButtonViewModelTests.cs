using System;
using Machine.Specifications;
using Moq;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Features.VideoCall.ViewModels;
using Prism.Regions;
using It = Machine.Specifications.It;

namespace PowerPuff.Features.VideoCall.Tests.ViewModels
{
    public class MainButtonViewModelTests
    {
        [Subject(typeof(MainButtonViewModel))]
        public class When_main_button_is_invoked
        {
            Establish context = () =>
            {
                _regionManagerMock = new Mock<IRegionManager>();
                _subject = new MainButtonViewModel(_regionManagerMock.Object);
            };

            private Because of = () => _subject.GoToVideoPageCommand.Execute();

            private It should_request_to_go_to_video_main_view =
                () =>
                    _regionManagerMock.Verify(
                        m =>
                            m.RequestNavigate(RegionNames.MainContentRegion, NavigableViews.Main.FeatureLayoutView.GetFullName(), Moq.It.IsAny<Action<NavigationResult>>()));

            private static MainButtonViewModel _subject;
            private static Mock<IRegionManager> _regionManagerMock;
        }
    }
}
