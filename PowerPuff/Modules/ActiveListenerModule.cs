using PowerPuff.Common;
using PowerPuff.Speech;
using PowerPuff.Views;
using Prism.Modularity;
using Prism.Regions;

namespace PowerPuff.Modules
{
    public class ActiveListenerModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IActiveListenerView _view;

        public ActiveListenerModule(IRegionManager regionManager, IActiveListenerView view, IActiveListener activeListener)
        {
            _regionManager = regionManager;
            _view = view;
            _view.OnListnerActivatorClick += activeListener.BeginActiveListening;
            activeListener.ActiveListeningStarted += _view.ActiveListeningStarted;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.ActiveListenerRegion, () => _view);
        }
    }
}
