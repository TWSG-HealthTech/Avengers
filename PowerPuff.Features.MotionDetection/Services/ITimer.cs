using System;

namespace PowerPuff.Features.MotionDetection.Services
{
    public interface ITimer
    {
        void Reset(TimeSpan timeOut);
        event Action Timeout;
    }

    public class Timer : ITimer
    {
        private System.Threading.Timer _timer;

        public void Reset(TimeSpan timeOut)
        {
            _timer?.Dispose();
            _timer = new System.Threading.Timer(state => Timeout?.Invoke(), null, timeOut, timeOut);
        }

        public event Action Timeout;
    }
}
