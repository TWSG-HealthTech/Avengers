using Machine.Specifications;
using Moq;
using PowerPuff.Features.Timer.Model;
using PowerPuff.Features.Timer.Sound;
using PowerPuff.Features.Timer.ViewModels;
using System;
using It = Machine.Specifications.It;

namespace PowerPuff.Features.Timer.Tests.ViewModels
{
    [Subject(typeof(TimerMainViewModel))]
    public class TimerMainViewModelTests
    {
        private static TimerMainViewModel _subject;
        private static Mock<TimerModel> _timerMock;
        private static Mock<IAlerter> _alerter;

        Establish baseContext = () =>
        {
            _alerter = new Mock<IAlerter>();
            _timerMock = new Mock<TimerModel>(new Mock<ITimer>().Object);
            _subject = new TimerMainViewModel(_timerMock.Object, _alerter.Object);
        };

        class OnInitialisation
        {
            It is_modifiable = () => _subject.IsModifiable.ShouldBeTrue();
            It is_startable = () => _subject.IsStartable.ShouldBeTrue();
        }

        class OnStarted
        {
            Because of = () => _timerMock.Raise(t => t.OnStarted += null);

            It is_not_modifiable = () => _subject.IsModifiable.ShouldBeFalse();
            It is_not_startable = () => _subject.IsStartable.ShouldBeFalse();
        }

        class OnPaused
        {
            Because of = () => _timerMock.Raise(t => t.OnPaused += null, new TimeSpan(1, 2, 3));

            It should_update_the_hours = () => _subject.Hours.ShouldEqual($"{1:D2}");
            It should_update_the_minutes = () => _subject.Minutes.ShouldEqual($"{2:D2}");
            It should_update_the_seconds = () => _subject.Seconds.ShouldEqual($"{3:D2}");

            It is_not_modifiable = () => _subject.IsModifiable.ShouldBeFalse();
            It is_startable = () => _subject.IsStartable.ShouldBeTrue();
        }

        class OnTick
        {
            Because of = () => _timerMock.Raise(t => t.OnTick += null, new TimeSpan(1, 2, 3));

            It should_update_the_hours = () => _subject.Hours.ShouldEqual($"{1:D2}");
            It should_update_the_minutes = () => _subject.Minutes.ShouldEqual($"{2:D2}");
            It should_update_the_seconds = () => _subject.Seconds.ShouldEqual($"{3:D2}");
        }

        class OnReset
        {
            Because of = () => _timerMock.Raise(t => t.OnReset += null, new TimeSpan(1, 2, 3));

            It should_update_the_hours = () => _subject.Hours.ShouldEqual($"{1:D2}");
            It should_update_the_minutes = () => _subject.Minutes.ShouldEqual($"{2:D2}");
            It should_update_the_seconds = () => _subject.Seconds.ShouldEqual($"{3:D2}");

            It is_modifiable = () => _subject.IsModifiable.ShouldBeTrue();
            It is_startable = () => _subject.IsStartable.ShouldBeTrue();
        }

        class OnCompleted
        {
            Because of = () => _timerMock.Raise(t => t.OnCompleted += null);

            It should_update_the_hours = () => _subject.Hours.ShouldEqual($"{0:D2}");
            It should_update_the_minutes = () => _subject.Minutes.ShouldEqual($"{0:D2}");
            It should_update_the_seconds = () => _subject.Seconds.ShouldEqual($"{0:D2}");

            It is_not_modifiable = () => _subject.IsModifiable.ShouldBeFalse();
            It is_not_startable = () => _subject.IsStartable.ShouldBeFalse();

            It plays_an_alert_sound = () => _alerter.Verify(a => a.Alert());
        }

        class When_Start_Button_is_Clicked
        {
            Because of = () => _subject.StartTimerButton.Execute();

            It tells_the_timer_to_start = () => _timerMock.Verify(t => t.Start());
        }

        class When_Pause_Button_is_Clicked
        {
            Because of = () => _subject.PauseTimerButton.Execute();

            It tells_the_timer_to_pause = () => _timerMock.Verify(t => t.Pause());
        }

        class When_Reset_Button_is_Clicked
        {
            Because of = () => _subject.ResetTimerButton.Execute();

            It tells_the_timer_to_reset = () => _timerMock.Verify(t => t.Reset());
        }

        class OnDurtionChanged
        {
            Because of = () => _timerMock.Raise(t => t.OnDurationChanged += null, new TimeSpan(1, 2, 3));

            It should_update_the_hours = () => _subject.Hours.ShouldEqual($"{1:D2}");
            It should_update_the_minutes = () => _subject.Minutes.ShouldEqual($"{2:D2}");
            It should_update_the_seconds = () => _subject.Seconds.ShouldEqual($"{3:D2}");
            
        }

        class When_Up_Button_for_seconds_is_clicked
        {
            Establish context = () =>
            {
                _timerMock.SetupProperty(t => t.Duration, TimeSpan.FromSeconds(2));
            };

            Because of = () => _subject.AddSecondsButton.Execute();

            It should_decrease_the_number_of_seconds = () => _timerMock.VerifySet(t => t.Duration = TimeSpan.FromSeconds(3));
        }

        class When_Down_Button_for_seconds_is_clicked
        {
            Establish context = () =>
            {
                _timerMock.SetupProperty(t => t.Duration, TimeSpan.FromSeconds(2));
            };

            Because of = () => _subject.SubtractSecondsButton.Execute();

            It should_decrease_the_number_of_seconds = () => _timerMock.VerifySet(t => t.Duration = TimeSpan.FromSeconds(1));
        }

        class When_Up_Button_for_minutes_is_clicked
        {
            Establish context = () =>
            {
                _timerMock.SetupProperty(t => t.Duration, TimeSpan.FromMinutes(2));
            };

            Because of = () => _subject.AddMinutesButton.Execute();

            It should_decrease_the_number_of_Minutes = () => _timerMock.VerifySet(t => t.Duration = TimeSpan.FromMinutes(3));
        }

        class When_Down_Button_for_minutes_is_clicked
        {
            Establish context = () =>
            {
                _timerMock.SetupProperty(t => t.Duration, TimeSpan.FromMinutes(2));
            };

            Because of = () => _subject.SubtractMinutesButton.Execute();

            It should_decrease_the_number_of_Minutes = () => _timerMock.VerifySet(t => t.Duration = TimeSpan.FromMinutes(1));
        }

        class When_Up_Button_for_hours_is_clicked
        {
            Establish context = () =>
            {
                _timerMock.SetupProperty(t => t.Duration, TimeSpan.FromHours(2));
            };

            Because of = () => _subject.AddHoursButton.Execute();

            It should_decrease_the_number_of_hours = () => _timerMock.VerifySet(t => t.Duration = TimeSpan.FromHours(3));
        }

        class When_Down_Button_for_hours_is_clicked
        {
            Establish context = () =>
            {
                _timerMock.SetupProperty(t => t.Duration, TimeSpan.FromHours(2));
            };

            Because of = () => _subject.SubtractHoursButton.Execute();

            It should_decrease_the_number_of_hours = () => _timerMock.VerifySet(t => t.Duration = TimeSpan.FromHours(1));
        }
    }
}
