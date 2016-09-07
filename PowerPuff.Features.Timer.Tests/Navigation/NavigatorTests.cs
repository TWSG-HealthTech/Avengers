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
            _subject = new Navigator(_regionManagerMock.Object, new TestDispatcher());
        };

        Because of = () => _subject.GoToPage("some page");

        It should_request_to_go_to_the_timer_page = () =>
            _regionManagerMock.Verify(
                m => m.RequestNavigate(RegionNames.MainContentRegion, NavigableViews.Main.FeatureLayoutView.GetFullName(), Moq.It.IsAny<Action<NavigationResult>>()));

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
