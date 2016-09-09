using Machine.Specifications;
using Moq;
using PowerPuff.Features.Timer.Model;
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

        Establish baseContext = () =>
        {
            _timerMock = new Mock<TimerModel>(new Mock<ITimer>().Object);
            _subject = new TimerMainViewModel(_timerMock.Object);
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

        class When_Start_Button_is_Clicked
        {
            Because of = () => _subject.StartTimerButton.Execute();

            It tell_the_timer_to_start = () => _timerMock.Verify(t => t.Start());
        }

        class When_Stop_Button_is_Clicked
        {
            Because of = () => _subject.StopTimerButton.Execute();

            It tell_the_timer_to_stop = () => _timerMock.Verify(t => t.Stop());
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
