using Machine.Specifications;
using Moq;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Features.Timer.Navigation;
using PowerPuff.Test.Helpers;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using It = Machine.Specifications.It;

namespace PowerPuff.Features.Timer.Tests.Navigation
{
    [Subject(typeof(Navigator))]
    class NavigatorTests
    {

        Establish context = () =>
        {
            _regionCollection = new TestRegionCollection();
            _regionManagerMock = new Mock<IRegionManager>();
            _regionManagerMock.SetupGet(m => m.Regions).Returns(_regionCollection);
            _regionManagerMock.Setup(
                m =>
                    m.RequestNavigate(Moq.It.IsAny<string>(), Moq.It.IsAny<string>(),
                        Moq.It.IsAny<Action<NavigationResult>>()))
                .Callback<string, string, Action<NavigationResult>>(
                    (x, y, callback) => callback(new NavigationResult(null, true)));
            _subject = new Navigator(_regionManagerMock.Object, new TestDispatcher());
        };


        class FeatureMainContentRegion_not_loaded
        {
            Because of = () => _subject.GoToPage("some page");

            It should_request_to_go_to_the_feature_layout = () =>
                _regionManagerMock.Verify(
                    m =>
                        m.RequestNavigate(RegionNames.MainContentRegion, NavigableViews.Main.FeatureLayoutView.GetFullName(),
                            Moq.It.IsAny<Action<NavigationResult>>()));

            It should_request_to_go_to_some_page = () =>
                _regionManagerMock.Verify(
                    m =>
                        m.RequestNavigate(RegionNames.FeatureMainContentRegion, "some page"));
        }

        class FeatureMainContentRegion_loaded
        {
            private Establish context = () =>
            {
                _regionCollection.Add(new Region {Name = RegionNames.FeatureMainContentRegion});
            };

            Because of = () => _subject.GoToPage("some page");

            It should_not_request_to_go_to_the_feature_layout = () =>
                _regionManagerMock.Verify(
                    m =>
                        m.RequestNavigate(RegionNames.MainContentRegion, NavigableViews.Main.FeatureLayoutView.GetFullName(),
                            Moq.It.IsAny<Action<NavigationResult>>()), Times.Never);

            It should_request_to_go_to_some_page = () =>
                _regionManagerMock.Verify(
                    m =>
                        m.RequestNavigate(RegionNames.FeatureMainContentRegion, "some page"));
        }

        protected static Mock<IRegionManager> _regionManagerMock;
        private static Navigator _subject;
        private static IRegionCollection _regionCollection;

        private class TestRegionCollection : Dictionary<string, IRegion>, IRegionCollection {
            public IEnumerator<IRegion> GetEnumerator()
            {
                return Values.GetEnumerator();
            }

            public event NotifyCollectionChangedEventHandler CollectionChanged;
            public void Add(IRegion region)
            {
                Add(region.Name, region);
            }

            public bool ContainsRegionWithName(string regionName)
            {
                return ContainsKey(regionName);
            }
        }
    }
}
