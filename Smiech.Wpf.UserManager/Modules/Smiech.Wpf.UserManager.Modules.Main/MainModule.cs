using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Smiech.Wpf.UserManager.Core;

namespace Smiech.Wpf.UserManager.Modules.Main
{
    public class MainModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public MainModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, "UserManager");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.UserManager>();
        }
    }
}