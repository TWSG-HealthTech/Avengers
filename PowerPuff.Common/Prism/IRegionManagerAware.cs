using Prism.Regions;

namespace PowerPuff.Common.Prism
{
    public interface IRegionManagerAware
    {
        IRegionManager RegionManager { get; set; }
    }
}
