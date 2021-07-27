using System;
using Prism.Ioc;
using Prism.Modularity;
using Smiech.Wpf.UserManager.Modules.Main;
using Smiech.Wpf.UserManager.Services;
using Smiech.Wpf.UserManager.Services.Interfaces;
using Smiech.Wpf.UserManager.Views;
using System.Windows;
using Smiech.Wpf.UserManager.Properties;

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
            containerRegistry.RegisterSingleton<IGoRestApiService>(x=>
                new GoRestApiService(new Uri(Settings.Default.GoRestApiBaseUrl), new BearerAuthenticator(Settings.Default.GoRestApiToken)));
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<MainModule>();
        }
    }
}
