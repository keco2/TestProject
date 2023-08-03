using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using TaskMgmt.Common;
using TaskMgmt.UI.ViewModel;
using Unity;

namespace TaskMgmt.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public App()
        {
            Logging.LoggingSetUp();
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            //ResolveUiDependecies();
        }

        private void ResolveUiDependecies()
        {
            IUnityContainer ioc = new UnityContainer();
            ioc
                .RegisterType<IProxy, Proxy>();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception)
            {
                HandleException(e.ExceptionObject as Exception);
            }
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            HandleException(e.Exception as Exception);
        }

        private void HandleException(Exception exception)
        {
            try
            {
                logger.Error(exception);
                MessageBox.Show("Something went wrong. Please find the error in the logfile.\n\nError preview:\n\n"
                    + exception.ToString().TakeLine(15)
                    + "}\n\n(See the full exception in the lofgile)", "Error");
            }
            finally
            {
                Shutdown(-1);
            }
        }

    }
}
