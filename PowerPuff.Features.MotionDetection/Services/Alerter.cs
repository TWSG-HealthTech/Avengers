using PowerPuff.Features.MotionDetection.Models;
using System;

namespace PowerPuff.Features.MotionDetection.Services
{
    public class Alerter
    {

        public Alerter(IMotionDetectionModel motionDetectionModel)
        {
            motionDetectionModel.Alarm += MotionDetectionModelOnAlarm;
        }

        private void MotionDetectionModelOnAlarm(DateTime lastSeenTime)
        {
            Console.WriteLine($"ALERT -- Last Seen at {lastSeenTime}");
        }
    }
}
