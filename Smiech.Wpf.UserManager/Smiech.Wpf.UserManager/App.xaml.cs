using Prism.Ioc;
using Prism.Modularity;
using Smiech.Wpf.UserManager.Modules.Main;
using Smiech.Wpf.UserManager.Services;
using Smiech.Wpf.UserManager.Services.Interfaces;
using Smiech.Wpf.UserManager.Views;
using System.Windows;

namespace Smiech.Wpf.UserManager
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
            containerRegistry.RegisterSingleton<IGoRestApiService, MessageService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<MainModule>();
        }
    }
}
