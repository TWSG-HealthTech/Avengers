using System;

namespace PowerPuff.Common.Logging
{
    public interface ILogger
    {
        void Info(string message);
        void Error(Exception ex, string message);
    }
}
