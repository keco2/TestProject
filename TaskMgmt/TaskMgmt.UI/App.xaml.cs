using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using TaskMgmt.Common;

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
                string msg = "Something went wrong. Please find the error in the logfile.";
#if DEBUG
                msg += $"\n\nError preview:\n\n{exception.ToString().TakeLines(15)}\n\n(See the full exception in the lofgile)";
#endif
                MessageBox.Show(msg, "Error");
            }
            finally
            {
                Shutdown(-1);
            }
        }

    }
}
