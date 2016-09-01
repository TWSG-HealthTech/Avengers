using System;
using Machine.Specifications;
using PowerPuff.Common;
using M = Moq;
using PowerPuff.Modules;
using PowerPuff.Speech;
using Prism.Regions;

namespace PowerPuff.Tests.Modules
{
    [Subject(typeof(ActiveListenerModule))]
    class ActiveListenerModuleTests
    {
        private static M.Mock<IRegionManager> _regionManagerMock;
        private static M.Mock<IActiveListenerView> _activeListenerViewMock;
        private static M.Mock<IActiveListener> _activeListenerMock;

        private static ActiveListenerModule _subject;

        Establish context = () =>
        {
            _regionManagerMock = new M.Mock<IRegionManager>();
            _activeListenerViewMock = new M.Mock<IActiveListenerView>();
            _activeListenerMock = new M.Mock<IActiveListener>();

            _subject = new ActiveListenerModule(_regionManagerMock.Object, _activeListenerViewMock.Object,
                _activeListenerMock.Object);
        };

        class Initialize
        {
            Because of = () => _subject.Initialize();

            It should_register_view_with_active_listener_region = () =>
            _regionManagerMock.Verify(manager => manager.RegisterViewWithRegion(RegionNames.ActiveListenerRegion, M.It.IsAny<Func<object>>()));
        }

        class OnListnerActivatorClick
        {
            Because of = () => _activeListenerViewMock.Raise(alv => alv.OnListnerActivatorClick += null);

            It should_tell_the_active_listener_to_start_listening = () =>
                _activeListenerMock.Verify(al => al.BeginActiveListening());
        }

        class OnActiveListeningStart
        {
            Because of = () => _activeListenerMock.Raise(al => al.ActiveListeningStarted += null);

            It should_tell_the_view_active_listening_is_started = () =>
                _activeListenerViewMock.Verify(alv => alv.ActiveListeningStarted());
        }

        class OnActiveListeningEnded
        {
            Because of = () => _activeListenerMock.Raise(al => al.ActiveListeningStopped += null);

            It should_tell_the_view_active_listening_is_stopped = () =>
                _activeListenerViewMock.Verify(alv => alv.ActiveListeningStopped());
        }
    }
}
