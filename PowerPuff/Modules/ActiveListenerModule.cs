using PowerPuff.Common;
using PowerPuff.Views;
using Prism.Modularity;
using Prism.Regions;

namespace PowerPuff.Modules
{
    class ActiveListenerModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly ActiveListenerView _view;

        public ActiveListenerModule(IRegionManager regionManager, ActiveListenerView view)
        {
            _regionManager = regionManager;
            _view = view;
        }

        public void Initialize()
        {
            IRegion activeListenerRegion = _regionManager.Regions[RegionNames.ActiveListenerRegion];
            activeListenerRegion.Add(_view);
        }
    }
}
