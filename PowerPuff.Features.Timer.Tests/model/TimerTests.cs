using Machine.Specifications;
using System;
using System.Collections.Generic;

namespace PowerPuff.Features.Timer.Tests.model
{
    [Subject(typeof(Model.Timer))]
    class TimerTests
    {
        private static Model.Timer _timer;
        private static IList<TimeSpan> _updatedValues;

        Establish context = () =>
        {
            _updatedValues = new List<TimeSpan>();
            _timer = new Model.Timer();
            _timer.OnTimeSpanChanged += (t) => _updatedValues.Add(t);
        };

        class Initialisation
        {
            It has_a_timer_of_zero = () => _timer.Duration.ShouldEqual(TimeSpan.Zero);
        }

        class Set_TimeSpan
        {
            Because of = () => _timer.Duration = new TimeSpan(1, 2, 3);

            It has_updateed_the_timer = () => _timer.Duration.ShouldEqual(new TimeSpan(1, 2, 3));
            It fires_timeSpanUpdated_event = () => _updatedValues.ShouldContainOnly(new TimeSpan(1, 2, 3));
        }

        class Set_TimeSpan_Negative
        {
            Because of = () => _timer.Duration = new TimeSpan(0, 0, -1);

            It has_not_updateed_the_timer = () => _timer.Duration.ShouldEqual(TimeSpan.Zero);
            It fires_timeSpanUpdated_event = () => _updatedValues.ShouldContainOnly();
        }
    }
}
