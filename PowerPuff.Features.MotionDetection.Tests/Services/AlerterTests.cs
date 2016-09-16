using Machine.Specifications;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Common.Navigation;
using PowerPuff.Features.MotionDetection.Models;
using PowerPuff.Features.MotionDetection.Services;
using System;
using M = Moq;

namespace PowerPuff.Features.MotionDetection.Tests.Services
{
    [Subject(typeof(Alerter))]
    class AlerterTests
    {
        private static M.Mock<INavigator> _navigatorMock;
        private static M.Mock<IMotionDetectionModel> _modelMock;
        private static Alerter _subject;

        private static DateTime _lastMotionTime = new DateTime(2016, 09, 16, 13, 24, 00);

        Establish context = () =>
        {
            _navigatorMock = new M.Mock<INavigator>();
            _modelMock = new M.Mock<IMotionDetectionModel>();
            _subject = new Alerter(_modelMock.Object, _navigatorMock.Object);
        };

        Because of = () => _modelMock.Raise(m => m.Alarm += null, _lastMotionTime);

        It goes_to_the_alarm_view = () => _navigatorMock.Verify(n => n.GoToPage(NavigableViews.MotionDetection.AlarmView.GetFullName()));
    }
}
