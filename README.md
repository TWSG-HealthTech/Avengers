# Avengers
CATE - Computerised Assistant To Everyone

![alt Build Status](https://ci.appveyor.com/api/projects/status/github/TWSG-HealthTech/Avengers?branch=master&retina=true "Build Status")

<a href="https://ci.appveyor.com/project/TWSGHealthTech/avengers" target="_blank">Latest Build</a>

## Project Structures
```
Build: contains build scripts
Source:
    PowerPuff: The main client application, contains the Shell and code to bootstrap the application
    PowerPuff.Common: Class library that contains common code for all client modules
    PowerPuff.Features.VideoCall: Video Call Module
Tests: mirror structure of Source folder
```

## Layout Structures

![alt Layout Structure](https://dl.dropboxusercontent.com/u/55034418/AvengersLayoutStructure.png "Layout Structure")

## Technical Details

### Navigation

Navigation in `Avengers` app is View-Based Navigation. To change a region from one view to another, following these steps:

(Assume the application is navigating from view `A1` in module `A` to view `B1` in module `B` in region with name `MainRegion`)
- Create a corresponding entry in an enum inside `NavigableViews` class for destination view `B1`. Nested enum inside this class represents each module (.e.g. `Main` enum represents views within main module)

    ```
    public static class NavigableViews
    {
        public enum B
        {
            B1
        }
    }
    ```

- In bootstrapper of module `B`, register `B1` as navigable to Autofac

    ```
    builder.RegisterTypeForNavigation<B1>(NavigableViews.B.B1.GetFullName());
    ```

- Request for navigation from module `A` using `IRegionManager`:

    ```
    _regionManager.RequestNavigate(RegionNames.MainRegion, NavigableViews.B.B1.GetFullName());
    ```

Note: If destination view itself include a region definition, when the application navigates to that destination view, the destination view is created again (but the old view is still kept by Prism), leading to duplicate region registration (causing exception `Region with the given name is already registered`). Solution is let destination viewmodel implements `IRegionMemberLifetime` and specify `false` for `KeepAlive`:

```
public class MainButtonsViewModel : IRegionMemberLifetime
{
    //...

    public bool KeepAlive { get; } = false;
}
```