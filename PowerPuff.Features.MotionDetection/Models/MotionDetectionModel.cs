using PowerPuff.Common.Helpers;
using PowerPuff.Features.MotionDetection.Services;
using System;

namespace PowerPuff.Features.MotionDetection.Models
{
    public class MotionDetectionModel : IMotionDetectionModel
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ITimer _timer;

        public MotionDetectionModel(IMotionDetector motionDetector, IDateTimeProvider dateTimeProvider, ITimer timer)
        {
            _dateTimeProvider = dateTimeProvider;
            _timer = timer;
            LastMotionTime = _dateTimeProvider.Now;
            motionDetector.MotionDetected += MotionDetectorOnMotionDetected;
            _timer.Timeout += () => Alarm?.Invoke(LastMotionTime);
        }

        private void MotionDetectorOnMotionDetected()
        {
            LastMotionTime = _dateTimeProvider.Now;
            _timer.Reset(TimeOut);
        }

        public TimeSpan TimeOut { get; set; } = TimeSpan.FromHours(10);
        public DateTime LastMotionTime { get; private set; }
        public event Action<DateTime> Alarm;
    }
}