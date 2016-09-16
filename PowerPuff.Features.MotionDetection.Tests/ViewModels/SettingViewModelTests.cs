using Machine.Specifications;
using PowerPuff.Features.MotionDetection.Models;
using PowerPuff.Features.MotionDetection.ViewModels;
using System;
using M = Moq;

namespace PowerPuff.Features.MotionDetection.Tests.ViewModels
{
    [Subject(typeof(SettingsViewModel))]
    class SettingViewModelTests
    {
        private static SettingsViewModel _subject;
        private static M.Mock<IMotionDetectionModel> _modelMock;
        
        private Establish baseContext = () =>
        {
            _modelMock = new M.Mock<IMotionDetectionModel>();
            _modelMock.SetupGet(m => m.TimeOut).Returns(TimeSpan.FromHours(1));
            _subject = new SettingsViewModel(_modelMock.Object);
        };

        private It get_the_initial_timeout_from_the_model =
            () => _subject.SelectedDuration.Duration.ShouldEqual(TimeSpan.FromHours(1));

        class DurationSelectionChanged
        {
            private static readonly DurationOption SelectedOption = new DurationOption { TimeText = "123", Duration = new TimeSpan(1, 2, 3) };

            private Establish context = () =>
            {
                _subject.SelectedDuration = SelectedOption;
            };

            Because of = () => _subject.DurationSelectionChanged.Execute();

            It sets_the_timeout_on_the_model = () => _modelMock.VerifySet(m => m.TimeOut = SelectedOption.Duration);
        }
    }
}
