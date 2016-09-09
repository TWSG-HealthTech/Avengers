using Machine.Specifications;
using Moq;
using PowerPuff.Features.Timer.Model;
using System;
using System.Collections.Generic;
using It = Machine.Specifications.It;

namespace PowerPuff.Features.Timer.Tests.model
{
    [Subject(typeof(TimerModel))]
    class TimerModelTests
    {
        private static TimerModel _timerModel;
        private static IList<TimeSpan> _updatedValues;
        private static IList<TimeSpan> _startTimes;
        private static IList<TimeSpan> _stopTimes;
        private static IList<TimeSpan> _tickTimes;
        private static IList<TimeSpan> _resetTimes;
        private static Mock<ITimer> _timerMock;
        private static int _completedInvokationCount;

        Establish baseContext = () =>
        {
            _completedInvokationCount = 0;
            _timerMock = new Mock<ITimer>();
            _updatedValues = new List<TimeSpan>();
            _startTimes = new List<TimeSpan>();
            _stopTimes = new List<TimeSpan>();
            _tickTimes = new List<TimeSpan>();
            _resetTimes = new List<TimeSpan>();
            _timerModel = new TimerModel(_timerMock.Object);
            _timerModel.OnDurationChanged += _updatedValues.Add;
            _timerModel.OnStarted += _startTimes.Add;
            _timerModel.OnStopped += _stopTimes.Add;
            _timerModel.OnTick += _tickTimes.Add;
            _timerModel.OnCompleted += () => ++_completedInvokationCount;
            _timerModel.OnReset += _resetTimes.Add;
        };

        class Initialisation
        {
            It has_a_timer_of_zero = () => _timerModel.Duration.ShouldEqual(TimeSpan.Zero);
        }

        class Set_TimeSpan
        {
            Because of = () => _timerModel.Duration = new TimeSpan(1, 2, 3);

            It has_updateed_the_timer = () => _timerModel.Duration.ShouldEqual(new TimeSpan(1, 2, 3));
            It fires_timeSpanUpdated_event = () => _updatedValues.ShouldContainOnly(new TimeSpan(1, 2, 3));
        }

        class Set_TimeSpan_Negative
        {
            Because of = () => _timerModel.Duration = new TimeSpan(0, 0, -1);

            It has_not_updateed_the_timer = () => _timerModel.Duration.ShouldEqual(TimeSpan.Zero);
            It fires_timeSpanUpdated_event = () => _updatedValues.ShouldContainOnly();
        }

        class Start_with_time
        {
            private Establish context = () =>
            {
                _timerModel.Duration = new TimeSpan(1, 2, 3);
            };

            Because of = () => _timerModel.Start();

            It fires_started_event = () => _startTimes.ShouldContainOnly(new TimeSpan(1, 2, 3));
        }

        class Start_with_zero_time
        {
            private Establish context = () =>
            {
                _timerModel.Duration = TimeSpan.Zero;
            };

            Because of = () => _timerModel.Start();

            It does_not_fire_started_event = () => _startTimes.ShouldContainOnly();
        }

        class Stop
        {
            private Establish context = () =>
            {
                _timerModel.Duration = TimeSpan.FromMinutes(1);
                _timerMock.Setup(t => t.Stop()).Returns(TimeSpan.FromSeconds(20));
            };

            Because of = () => _timerModel.Stop();

            It stops_the_timer = () => _timerMock.Verify(t => t.Stop());

            It fires_stopped_event_with_remaining_time = () => _stopTimes.ShouldContainOnly(TimeSpan.FromSeconds(40));
        }

        class Tick
        {
            private Establish context = () =>
            {
                _timerModel.Duration = TimeSpan.FromMinutes(1);
            };

            Because of = () => _timerMock.Raise(t => t.OnTick += null, TimeSpan.FromSeconds(20));

            It fires_tick_event_with_remaining_time = () => _tickTimes.ShouldContainOnly(TimeSpan.FromSeconds(40));
        }

        class Tick_Finished
        {
            private Establish context = () =>
            {
                _timerModel.Duration = TimeSpan.FromMinutes(1);
            };

            Because of = () => _timerMock.Raise(t => t.OnTick += null, TimeSpan.FromMinutes(1));

            It fires_no_tick_event = () => _tickTimes.ShouldContainOnly();
            It fires_completed_event = () => _completedInvokationCount.ShouldEqual(1);
            It stops_the_timer = () => _timerMock.Verify(t => t.Stop());
        }

        class Reset
        {
            private Establish context = () =>
            {
                _timerModel.Duration = TimeSpan.FromMinutes(1);
            };

            Because of = () => _timerModel.Reset();

            It resets_the_timer = () => _timerMock.Verify(t => t.Reset());
            It fires_reset_event = () => _resetTimes.ShouldContainOnly(TimeSpan.FromMinutes(1));
        }
    }
}
