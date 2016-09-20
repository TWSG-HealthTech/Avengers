using System;

namespace PowerPuff.Features.MotionDetection.Services
{
    public interface IMotionDetector
    {
        event Action MotionDetected;
    }
}