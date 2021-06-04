using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism8Puzzle.Core;
using Puzzle.Views;

namespace Puzzle
{
    public class PuzzleModule : IModule
    {
        readonly IRegionManager _regionManager;

        public PuzzleModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(PuzzleView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}