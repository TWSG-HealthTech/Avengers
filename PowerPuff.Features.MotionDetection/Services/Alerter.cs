using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Common.Navigation;
using PowerPuff.Features.MotionDetection.Models;
using System;

namespace PowerPuff.Features.MotionDetection.Services
{
    public class Alerter
    {
        private readonly INavigator _navigator;
        private readonly ISoundPlayer _soundPlayer;

        public Alerter(IMotionDetectionModel motionDetectionModel, INavigator navigator, ISoundPlayer soundPlayer)
        {
            _navigator = navigator;
            _soundPlayer = soundPlayer;
            motionDetectionModel.Alarm += MotionDetectionModelOnAlarm;
        }

        private void MotionDetectionModelOnAlarm(DateTime lastSeenTime)
        {
            _navigator.GoToPage(NavigableViews.MotionDetection.AlarmView.GetFullName());
            _soundPlayer.Play(Properties.Resources.warning);
        }
    }
}
