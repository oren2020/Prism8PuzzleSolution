using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism8Puzzle.Core;
using ToolBar.Views;

namespace ToolBar
{
    public class ToolBarModule : IModule
    {
        readonly IRegionManager _regionManager;

        public ToolBarModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(ToolBarView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}