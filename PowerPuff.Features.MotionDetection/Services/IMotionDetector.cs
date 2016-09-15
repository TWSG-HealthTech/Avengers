using System;

namespace PowerPuff.Features.MotionDetection.Services
{
    public class MotionDetector : IMotionDetector {
        public event Action MotionDetected;
    }

    public interface IMotionDetector
    {
        event Action MotionDetected;
    }
}