using Machine.Specifications;
using Moq;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.ViewModels;
using Prism.Regions;
using It = Machine.Specifications.It;

namespace PowerPuff.Tests.ViewModels
{
    public class FeatureLayoutViewModelTests
    {
        [Subject(typeof(FeatureLayoutViewModel))]
        public class When_back_button_is_clicked
        {
            Establish context = () =>
            {
                _regionManagerMock = new Mock<IRegionManager>();
                _subject = new FeatureLayoutViewModel(_regionManagerMock.Object);
            };

            Because of = () => _subject.GoBackCommand.Execute();

            private It should_request_to_go_back_to_home_view =
                () =>
                    _regionManagerMock.Verify(
                        m => m.RequestNavigate(RegionNames.MainContentRegion, NavigableViews.Main.HomeView.GetFullName()));

            private static Mock<IRegionManager> _regionManagerMock;
            private static FeatureLayoutViewModel _subject;
        }
    }
}
