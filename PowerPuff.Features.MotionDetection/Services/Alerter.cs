using PowerPuff.Common.Navigation;
using PowerPuff.Features.MotionDetection.Models;
using System;
using System.Media;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;

namespace PowerPuff.Features.MotionDetection.Services
{
    public class Alerter
    {
        private readonly INavigator _navigator;

        public Alerter(IMotionDetectionModel motionDetectionModel, INavigator navigator)
        {
            _navigator = navigator;
            motionDetectionModel.Alarm += MotionDetectionModelOnAlarm;
        }

        private void MotionDetectionModelOnAlarm(DateTime lastSeenTime)
        {
            _navigator.GoToPage(NavigableViews.MotionDetection.AlarmView.GetFullName());
            var player = new SoundPlayer(Properties.Resources.warning);
            player.Play();
        }
    }
}
