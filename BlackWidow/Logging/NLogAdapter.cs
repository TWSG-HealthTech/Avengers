using System;
using NLog;

namespace BlackWidow.Logging
{
    public class NLogAdapter : ILogger
    {
        private static readonly Logger _logger = LogManager.GetLogger("AvengersLogger");

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Error(Exception ex, string message = null)
        {
            _logger.Fatal(ex, message);
        }
    }
}
