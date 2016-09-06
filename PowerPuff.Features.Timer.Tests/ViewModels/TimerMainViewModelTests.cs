using Machine.Specifications;
using PowerPuff.Features.Timer.ViewModels;

namespace PowerPuff.Features.Timer.Tests.ViewModels
{
    [Subject(typeof(TimerMainViewModel))]
    public class TimerMainViewModelTests
    {
        private static TimerMainViewModel _subject;

        Establish context = () =>
        {
            _subject = new TimerMainViewModel();
        };

        class When_Start_Button_is_Clicked
        {
            Because of = () => _subject.StartTimerButton.Execute();

            It should_toggle_timer_to_be_enabled = () => _subject.IsTimerEnabled.ShouldEqual(true);
        }

        class When_Stop_Button_is_Clicked
        {
            Establish stopContext = () =>
            {
                _subject.StartTimerButton.Execute();
            };

            Because of = () => _subject.StopTimerButton.Execute();

            It should_toggle_timer_to_be_disabled = () => _subject.IsTimerEnabled.ShouldEqual(false);
        }

        class When_Up_Button_for_seconds_is_clicked
        {
            Because of = () => _subject.AddSecondsButton.Execute();

            It should_increase_the_number_of_seconds = () => _subject.Seconds.ShouldEqual($"{1:D2}");
        }

        class When_Down_Button_for_seconds_is_clicked
        {
            Establish stopContext = () =>
            {
                _subject.AddSecondsButton.Execute();
            };

            Because of = () => _subject.SubtractSecondsButton.Execute();

            It should_decrease_the_number_of_seconds = () => _subject.Seconds.ShouldEqual($"{0:D2}");
        }

        class When_Up_Button_for_minutes_is_clicked
        {
            Because of = () => _subject.AddMinutesButton.Execute();

            It should_increase_the_number_of_minutes = () => _subject.Minutes.ShouldEqual($"{1:D2}");
        }

        class When_Down_Button_for_minutes_is_clicked
        {
            Establish stopContext = () =>
            {
                _subject.AddMinutesButton.Execute();
            };

            Because of = () => _subject.SubtractMinutesButton.Execute();

            It should_decrease_the_number_of_Minutes = () => _subject.Minutes.ShouldEqual($"{0:D2}");
        }

        class When_Up_Button_for_hours_is_clicked
        {
            Because of = () => _subject.AddHoursButton.Execute();

            It should_increase_the_number_of_hours = () => _subject.Hours.ShouldEqual($"{1:D2}");
        }

        class When_Down_Button_for_hours_is_clicked
        {
            Establish stopContext = () =>
            {
                _subject.AddHoursButton.Execute();
            };

            Because of = () => _subject.SubtractHoursButton.Execute();

            It should_decrease_the_number_of_hours = () => _subject.Hours.ShouldEqual($"{0:D2}");
        }
    }
}
