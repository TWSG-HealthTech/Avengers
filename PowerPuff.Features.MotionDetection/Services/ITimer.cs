using System;

namespace PowerPuff.Features.MotionDetection.Services
{
    public interface ITimer
    {
        void Reset(TimeSpan timeOut);
    }
}
