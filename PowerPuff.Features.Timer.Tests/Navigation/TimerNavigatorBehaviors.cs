using Machine.Specifications;
using Moq;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using Prism.Regions;
using System;
using It = Machine.Specifications.It;

namespace PowerPuff.Features.Timer.Tests.Navigation
{
    [Behaviors]
    class TimerNavigatorBehaviors
    {
        It should_request_to_go_to_the_timer_page = () =>
            _regionManagerMock.Verify(
                m => m.RequestNavigate(RegionNames.MainContentRegion, NavigableViews.Main.FeatureLayoutView.GetFullName(), Moq.It.IsAny<Action<NavigationResult>>()));

        protected static Mock<IRegionManager> _regionManagerMock;
    }
}
