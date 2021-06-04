using Navigation;
using Prism.Ioc;
using Prism.Modularity;
using Prism8Puzzle.Views;
using Puzzle;
using Services;
using StatusBar;
using System.Windows;
using ToolBar;

namespace Prism8Puzzle
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);

            _ = moduleCatalog.AddModule(typeof(ServicesModule));
            moduleCatalog.AddModule(typeof(PuzzleModule));
            moduleCatalog.AddModule(typeof(ToolBarModule));
            moduleCatalog.AddModule(typeof(StatusBarModule));
            moduleCatalog.AddModule(typeof(NavigationModule));

        }
    }
}
