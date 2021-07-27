using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Smiech.Wpf.UserManager.Core;
using Smiech.Wpf.UserManager.Modules.Main.Views;

namespace Smiech.Wpf.UserManager.Modules.Main
{
    public class ModuleNameModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ModuleNameModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, "ViewA");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA>();
        }
    }
}