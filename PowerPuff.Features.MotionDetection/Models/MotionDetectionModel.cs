using System;
using PowerPuff.Common.Helpers;
using PowerPuff.Features.MotionDetection.Services;

namespace PowerPuff.Features.MotionDetection.Models
{
    public class MotionDetectionModel : IMotionDetectionModel
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public MotionDetectionModel(IMotionDetector motionDetector, IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
            LastMotionTime = _dateTimeProvider.Now;
            motionDetector.MotionDetected += MotionDetectorOnMotionDetected;
        }

        private void MotionDetectorOnMotionDetected()
        {
            LastMotionTime = _dateTimeProvider.Now;
        }

        public TimeSpan TimeOut { get; set; }
        public DateTime LastMotionTime { get; private set; }
    }
}