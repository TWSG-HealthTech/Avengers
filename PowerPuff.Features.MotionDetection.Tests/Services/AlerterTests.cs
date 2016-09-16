using Machine.Specifications;
using Moq;
using PowerPuff.Features.MotionDetection.Models;
using PowerPuff.Features.MotionDetection.Services;
using System;

namespace PowerPuff.Features.MotionDetection.Tests.Services
{
    [Subject(typeof(Alerter))]
    class AlerterTests
    {
        private static Mock<IMotionDetectionModel> _modelMock;
        private static Alerter _subject;

        private static DateTime _lastMotionTime = new DateTime(2016, 09, 16, 13, 24, 00);

        Establish context = () =>
        {
            _modelMock = new Mock<IMotionDetectionModel>();
            _subject = new Alerter(_modelMock.Object);
        };

        Because of = () => _modelMock.Raise(m => m.Alarm += null, _lastMotionTime);
    }
}
