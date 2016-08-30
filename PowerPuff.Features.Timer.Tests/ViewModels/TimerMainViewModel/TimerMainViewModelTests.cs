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
            Because of = () => _subject.ButtonStartTimer.Execute();

            It should_toggle_timer_to_be_eneabled = () => _subject.IsTimerEnabled.ShouldEqual(true.ToString());
        }

        class When_Stop_Button_is_Clicked
        {
            Establish stopContext = () =>
            {
                _subject.ButtonStartTimer.Execute();
            };

            Because of = () => _subject.ButtonStopTimer.Execute();

            It should_toggle_timer_to_be_disabled = () => _subject.IsTimerEnabled.ShouldEqual(false.ToString());
        }
    }
}
