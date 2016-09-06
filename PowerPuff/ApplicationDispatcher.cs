using System;
using System.Windows;
using PowerPuff.Common.Helpers;

namespace PowerPuff
{
    class ApplicationDispatcher : IDispatcher
    {
        public void Invoke(Action callback)
        {
            Application.Current.Dispatcher.Invoke(callback);
        }
    }
}
