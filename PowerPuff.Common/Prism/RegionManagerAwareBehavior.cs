using System;
using System.Collections.Specialized;
using System.Windows;
using Prism.Regions;

namespace PowerPuff.Common.Prism
{
    public class RegionManagerAwareBehavior : RegionBehavior
    {
        public const string BehaviorKey = "RegionManagerAwareBehavior";

        protected override void OnAttach()
        {
            Region.Views.CollectionChanged += ViewsOnCollectionChanged;
        }

        private void ViewsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var item in e.NewItems)
                    {
                        var regionManager = Region.RegionManager;

                        //If the view is created with scope region manager, the behavior uses that region manager
                        var element = item as FrameworkElement;
                        var scopedRegionManager = element?.GetValue(RegionManager.RegionManagerProperty) as IRegionManager;
                        if (scopedRegionManager != null)
                        {
                            regionManager = scopedRegionManager;
                        }

                        InvokeOnRegionManagerAwareElement(item, x => x.RegionManager = regionManager);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems)
                    {
                        InvokeOnRegionManagerAwareElement(item, x => x.RegionManager = null);
                    }
                    break;
            }
        }

        private void InvokeOnRegionManagerAwareElement(object item, Action<IRegionManagerAware> invocation)
        {
            var regionManagerAwareItem = item as IRegionManagerAware;
            if (regionManagerAwareItem != null)
            {
                invocation(regionManagerAwareItem);
            }

            var element = item as FrameworkElement;
            var regionManagerAwareDataContext = element?.DataContext as IRegionManagerAware;
            if (regionManagerAwareDataContext == null) return;

            // If a view doesn't have a data context (view model) it will inherit the data context from the parent view.
            // The following check is done to avoid setting the RegionManager property in the view model of the parent view by mistake. 
            var elementParent = element.Parent as FrameworkElement;
            var regionManagerAwareDataContextParent = elementParent?.DataContext as IRegionManagerAware;
            if (regionManagerAwareDataContextParent != null &&
                regionManagerAwareDataContext == regionManagerAwareDataContextParent)
            {
                // If all of the previous conditions are true, it means that this view doesn't have a view model
                // and is using the view model of its visual parent.
                return;
            }

            // If any of the previous conditions is false, the view has its own view model and it implements IRegionManagerAware
            invocation(regionManagerAwareDataContext);
        }
    }
}
