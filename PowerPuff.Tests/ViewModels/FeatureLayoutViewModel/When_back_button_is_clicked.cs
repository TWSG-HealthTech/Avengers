using Machine.Specifications;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using Prism.Regions;
using SUT = PowerPuff.ViewModels;
using M = Moq;

namespace PowerPuff.Tests.ViewModels.FeatureLayoutViewModel
{    
    [Subject(typeof(SUT.FeatureLayoutViewModel))]
    public class When_back_button_is_clicked
    {
        Establish context = () =>
        {
            _regionManagerMock = new M.Mock<IRegionManager>();
            _subject = new SUT.FeatureLayoutViewModel(_regionManagerMock.Object);
        };

        Because of = () => _subject.GoBackCommand.Execute();

        private It should_request_to_go_back_to_home_view =
            () =>
                _regionManagerMock.Verify(
                    m => m.RequestNavigate(RegionNames.MainContentRegion, NavigableViews.Main.HomeView.GetFullName()));

        private static M.Mock<IRegionManager> _regionManagerMock;
        private static SUT.FeatureLayoutViewModel _subject;
    }
}
