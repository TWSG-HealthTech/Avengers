using PowerPuff.Common.Helpers;
using PowerPuff.Features.MotionDetection.Services;
using System;

namespace PowerPuff.Features.MotionDetection.Models
{
    public class MotionDetectionModel : IMotionDetectionModel
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ITimer _timer;
        private TimeSpan _timeOut = TimeSpan.FromHours(10);

        public MotionDetectionModel(IMotionDetector motionDetector, IDateTimeProvider dateTimeProvider, ITimer timer)
        {
            _dateTimeProvider = dateTimeProvider;
            _timer = timer;
            LastMotionTime = _dateTimeProvider.Now;
            motionDetector.MotionDetected += MotionDetectorOnMotionDetected;
            _timer.Timeout += () => Alarm?.Invoke(LastMotionTime);
            _timer.Reset(TimeOut);
        }

        private void MotionDetectorOnMotionDetected()
        {
            LastMotionTime = _dateTimeProvider.Now;
            _timer.Reset(TimeOut);
        }

        public TimeSpan TimeOut
        {
            get { return _timeOut; }
            set
            {
                _timeOut = value;
                _timer.Reset(_timeOut);
            }
        }

        public DateTime LastMotionTime { get; private set; }
        public event Action<DateTime> Alarm;
    }
}