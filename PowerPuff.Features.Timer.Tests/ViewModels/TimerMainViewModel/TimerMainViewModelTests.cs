using System;
using PowerPuff.Common;
using SUT = PowerPuff.Features.Timer.ViewModels;
using M = Moq;
using Machine.Specifications;

namespace PowerPuff.Features.Timer.Tests.ViewModels.TimerMainViewModel
{
    [Subject(typeof(SUT.TimerMainViewModel))]
    class TimerMainViewModelTests
    {
        private static SUT.TimerMainViewModel _subject;

        Establish context = () =>
        {
            _subject = new SUT.TimerMainViewModel();
        };

        class When_Start_Button_is_Clicked
        {
            Because of = () => _subject.StartTimerButton.Execute();

            It should_toggle_timer_to_be_enabled = () => _subject.IsTimerEnabled.ShouldEqual(true.ToString());
        }

        class When_Stop_Button_is_Clicked
        {
            Establish stopContext = () =>
            {
                _subject.StartTimerButton.Execute();
            };

            Because of = () => _subject.StopTimerButton.Execute();

            It should_toggle_timer_to_be_disabled = () => _subject.IsTimerEnabled.ShouldEqual(false.ToString());
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
    }
}
