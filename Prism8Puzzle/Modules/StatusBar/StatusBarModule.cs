using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism8Puzzle.Core;
using StatusBar.Views;

namespace StatusBar
{
    public class StatusBarModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public StatusBarModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _ = _regionManager.RegisterViewWithRegion(RegionNames.StatusBarRegion, typeof(StatusBarView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}