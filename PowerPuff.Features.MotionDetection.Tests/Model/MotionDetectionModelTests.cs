using Machine.Specifications;
using PowerPuff.Features.MotionDetection.Models;
using PowerPuff.Features.MotionDetection.Services;
using PowerPuff.Test.Helpers;
using System;
using System.Collections.Generic;
using M = Moq;

namespace PowerPuff.Features.MotionDetection.Tests.Model
{
    [Subject(typeof(MotionDetectionModel))]
    class MotionDetectionModelTests
    {
        private static M.Mock<IMotionDetector> _motionDetectorMock;
        private static M.Mock<ITimer> _timer;
        private static MotionDetectionModel _subject;
        private static TestTimeProvider _testTimeProvider;
        private static DateTime _now = new DateTime(2016, 09, 15, 09, 00, 00);
        private static IList<DateTime> _alarmList;

        Establish baseContext = () =>
        {
            _alarmList = new List<DateTime>();
            _testTimeProvider = new TestTimeProvider {Now = _now};
            _timer = new M.Mock<ITimer>();
            _motionDetectorMock = new M.Mock<IMotionDetector>();
            _subject = new MotionDetectionModel(_motionDetectorMock.Object, _testTimeProvider, _timer.Object);
            _subject.Alarm += _alarmList.Add;
        };

        class Contruction
        {
            It sets_the_LastMotionTime_to_now = () => _subject.LastMotionTime.ShouldEqual(_now);
            It sets_the_default_timeout_to_ten_hours = () => _subject.TimeOut.ShouldEqual(TimeSpan.FromHours(10));
            It resets_the_timer = () => _timer.Verify(t => t.Reset(TimeSpan.FromHours(10)));
        }

        class set_Timeout
        {
            Because of = () => _subject.TimeOut = TimeSpan.FromHours(20);

            It sets_the_default_timeout_to_ten_hours = () => _subject.TimeOut.ShouldEqual(TimeSpan.FromHours(20));
            It resets_the_timer = () => _timer.Verify(t => t.Reset(TimeSpan.FromHours(20)));
        }

        class MotionDetected
        {
            Establish context = () =>
            {
                M.MockExtensions.ResetCalls(_timer);
                _testTimeProvider.Now = _newNow;
            };

            Because of = () => _motionDetectorMock.Raise(m => m.MotionDetected += null);

            It updates_the_LastMotionTime = () => _subject.LastMotionTime.ShouldEqual(_newNow);
            It resets_the_timer = () => _timer.Verify(t => t.Reset(TimeSpan.FromHours(10)));

            private static DateTime _newNow = new DateTime(2016, 09, 15, 17, 43, 00);
        }

        class Timeout
        {
            Because of = () => _timer.Raise(t => t.Timeout += null);

            It rasies_an_alarm_event = () => _alarmList.ShouldContainOnly(_now);
        }
    }
}
