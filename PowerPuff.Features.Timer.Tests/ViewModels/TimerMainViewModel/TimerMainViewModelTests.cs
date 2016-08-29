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
        private static int Answer;

        Establish context = () =>
        {
            _subject = new SUT.TimerMainViewModel();
        };
    }
}
