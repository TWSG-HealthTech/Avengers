using Machine.Specifications;
using PowerPuff.Features.MotionDetection.Models;
using PowerPuff.Features.MotionDetection.ViewModels;
using System;
using M = Moq;

namespace PowerPuff.Features.MotionDetection.Tests.ViewModels
{
    [Subject(typeof(AlarmViewModel))]
    class AlarmViewModelTests
    {
        private static M.Mock<IMotionDetectionModel> _model;
        private static AlarmViewModel _subject;
        private static DateTime _now = DateTime.Now;

        Establish context = () =>
        {

            _model = new M.Mock<IMotionDetectionModel>();
            _model.SetupGet(m => m.LastMotionTime).Returns(_now);
            _subject = new AlarmViewModel(_model.Object);
        };

        It indicates_the_last_seen_time = () => _subject.LastSeen.ShouldEqual(_now);
    }
}
