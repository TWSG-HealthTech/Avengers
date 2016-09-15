using Machine.Specifications;
using PowerPuff.Features.MotionDetection.Models;
using PowerPuff.Features.MotionDetection.Services;
using PowerPuff.Test.Helpers;
using System;
using M = Moq;

namespace PowerPuff.Features.MotionDetection.Tests.Model
{
    [Subject(typeof(MotionDetectionModel))]
    class MotionDetectionModelTests
    {
        private static M.Mock<IMotionDetector> _motionDetectorMock;
        private static MotionDetectionModel _subject;
        private static TestTimeProvider _testTimeProvider;
        private static DateTime _now = new DateTime(2016, 09, 15, 09, 00, 00);

        Establish baseContext = () =>
        {
            _testTimeProvider = new TestTimeProvider {Now = _now};
            _motionDetectorMock = new M.Mock<IMotionDetector>();
            _subject = new MotionDetectionModel(_motionDetectorMock.Object, _testTimeProvider);
        };

        class Contruction
        {
            It sets_the_LastMotionTime_to_now = () => _subject.LastMotionTime.ShouldEqual(_now);
        }

        class MotionDetected
        {
            Establish context = () => _testTimeProvider.Now = _newNow;

            Because of = () => _motionDetectorMock.Raise(m => m.MotionDetected += null);

            It updates_the_LastMotionTime = () => _subject.LastMotionTime.ShouldEqual(_newNow);

            private static DateTime _newNow = new DateTime(2016, 09, 15, 17, 43, 00);
        }
    }
}
