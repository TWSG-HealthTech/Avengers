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

        class When_Start_Button_Is_Clicked
        {
            Because of = () => _subject.ButtonStartTimer.Execute();

            It should_toggle_timer_to_be_eneabled = () => _subject.IsTimerEnabled.ShouldEqual("True");
        }
    }
}
