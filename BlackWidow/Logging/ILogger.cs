using System;

namespace BlackWidow.Logging
{
    public interface ILogger
    {
        void Info(string message);
        void Error(Exception ex, string message);
    }
}
