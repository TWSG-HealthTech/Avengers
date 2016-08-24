using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using BlackWidow.Logging;

namespace PowerPuff
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();

            var logger = bootstrapper.Container.Resolve<ILogger>();

            //Unhandled exceptions from all threads in the AppDomain
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                LogUnhandledException(logger, (Exception)e.ExceptionObject, "AppDomain.CurrentDomain.UnhandledException");

            //Unhandled exceptions from main UI dispatcher thread
            DispatcherUnhandledException += (s, e) =>
            {
                e.Handled = true;
                LogUnhandledException(logger, e.Exception,
                    "Application.Current.DispatcherUnhandledException");
            };

            //Unhandled exceptions from within each AppDomain that uses a task scheduler for async operations
            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                e.SetObserved();
                LogUnhandledException(logger, e.Exception, "TaskScheduler.UnobservedTaskException");
            };
        }

        private void LogUnhandledException(ILogger logger, Exception exception, string eventName)
        {
            logger.Error(exception, eventName);
            MessageBox.Show($"An exception occurred: {exception}");
        }
    }
}
