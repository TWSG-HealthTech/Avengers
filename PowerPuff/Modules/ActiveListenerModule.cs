using PowerPuff.Common;
using Prism.Modularity;
using Prism.Regions;

namespace PowerPuff.Modules
{
    class ActiveListenerModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ActiveListenerModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            IRegion activeListenerRegion = _regionManager.Regions[RegionNames.ActiveListenerRegion];
            activeListenerRegion.Add(new Views.ActiveListenerView());
        }
    }
}
