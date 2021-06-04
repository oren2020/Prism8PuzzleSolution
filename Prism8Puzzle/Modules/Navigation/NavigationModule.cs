using Navigation.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism8Puzzle.Core;

namespace Navigation
{
    public class NavigationModule : IModule
    {
        readonly IRegionManager _regionManager;

        public NavigationModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, typeof(NavigationView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}