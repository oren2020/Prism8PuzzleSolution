using Prism.Ioc;
using Prism.Modularity;
using Prism8Puzzle.Core;

namespace Services
{
    public class ServicesModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IPuzzleService, PuzzleService>();
        }
    }
}