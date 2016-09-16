using PowerPuff.Features.MotionDetection.Models;
using System;

namespace PowerPuff.Features.MotionDetection.ViewModels
{
    public class AlarmViewModel
    {
        private readonly IMotionDetectionModel _model;

        public AlarmViewModel(IMotionDetectionModel model)
        {
            _model = model;
        }

        public DateTime LastSeen => _model.LastMotionTime;
    }
}
