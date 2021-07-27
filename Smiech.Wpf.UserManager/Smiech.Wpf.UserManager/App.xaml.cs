using System;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Modularity;
using Smiech.Wpf.UserManager.Modules.Main;
using Smiech.Wpf.UserManager.Services;
using Smiech.Wpf.UserManager.Services.Interfaces;
using Smiech.Wpf.UserManager.Views;
using System.Windows;
using System.Windows.Threading;
using Serilog;
using Serilog.Events;
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

        protected override void OnStartup(StartupEventArgs e)
        {
            // Configure Serilog and the sinks at the startup of the app
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.File(path: "Smiech.Wpf.UserManager.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            DispatcherUnhandledException += App_OnDispatcherUnhandledException;

            base.OnStartup(e);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Logger.Error(e.ExceptionObject as Exception, $"UnhandledException");
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Log.Logger.Error(e.Exception, $"UnobservedTaskException");
        }

        protected override void InitializeModules()
        {
            var manager = Container.Resolve<IModuleManager>();
            manager.LoadModuleCompleted += LoadModuleCompleted;
            manager.Run();
        }

        private void LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            LoadModuleCompleted(e.ModuleInfo, e.Error, e.IsErrorHandled);
        }

        protected virtual void LoadModuleCompleted(IModuleInfo moduleInfo, Exception error, bool isHandled)
        {
            if (error != null && error is ContainerResolutionException cre)
            {
                var errors = cre.GetErrors();
                foreach ((var type, var ex) in errors)
                {
                    Log.Logger.Error(ex, $"Error with: {type.FullName}");
                }
            }
        }

        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Log.Logger.Error(e.Exception, "DispatcherUnhandledException");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Flush all Serilog sinks before the app closes
            Log.CloseAndFlush();

            base.OnExit(e);
        }
    }
}
