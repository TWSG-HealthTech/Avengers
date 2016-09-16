using System;

namespace PowerPuff.Features.MotionDetection.Services
{
    public interface ITimer
    {
        void Reset(TimeSpan timeOut);
        event Action Timeout;
    }

    public class Timer : ITimer {
        public void Reset(TimeSpan timeOut)
        {
        }

        public event Action Timeout;
    }
}
