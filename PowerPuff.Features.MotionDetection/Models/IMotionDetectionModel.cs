using System;

namespace PowerPuff.Features.MotionDetection.Models
{
    public interface IMotionDetectionModel
    {
        TimeSpan TimeOut { get; set; }
    }
}
