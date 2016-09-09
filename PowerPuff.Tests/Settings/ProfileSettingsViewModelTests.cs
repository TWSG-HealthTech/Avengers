using System;
using Machine.Specifications;
using PowerPuff.Settings;
using Prism.Regions;
using M = Moq;

namespace PowerPuff.Tests.Settings
{
    [Subject(typeof(ProfileSettingsViewModel))]
    public class ProfileSettingsViewModelTests
    {
        private static ProfileSettingsViewModel _subject;
        private static M.Mock<IProfileGateway> _gatewayMock;

        private Establish context = () =>
        {
            _gatewayMock = new M.Mock<IProfileGateway>();
            _subject = new ProfileSettingsViewModel(_gatewayMock.Object);
        };

        public class When_navigate_to_profile_tab
        {
            private Because of = NavigateTo;

            private It should_request_for_profile_information =
                () => _gatewayMock.Verify(g => g.GetProfileBy(M.It.IsAny<string>()));

            private static void NavigateTo()
            {
                var regionNavigationServiceMock = new M.Mock<IRegionNavigationService>();
                var context = new NavigationContext(regionNavigationServiceMock.Object, new Uri("some uri", UriKind.RelativeOrAbsolute));
                _subject.OnNavigatedTo(context);
            }
        }

        public class When_update_button_is_clicked_and_connection_is_missing_name
        {
            private Establish context = () =>
            {
                _subject.SelectedConnection = new SocialConnection
                {
                    Id = 1,
                    Name = ""
                };
            };

            Because of = () => _subject.UpdateConnectionCommand.Execute();

            It should_ignore_request =
                () =>
                    _gatewayMock.Verify(g => g.UpdateConnection(M.It.IsAny<string>(), _subject.SelectedConnection),
                        M.Times.Never);
        }

        public class When_update_button_is_clicked_and_connection_is_valid
        {
            private Establish context = () =>
            {
                _subject.SelectedConnection = new SocialConnection
                {
                    Id = 1,
                    Name = "some name"
                };
            };

            Because of = () => _subject.UpdateConnectionCommand.Execute();

            It should_request_server_update =
                () =>
                    _gatewayMock.Verify(g => g.UpdateConnection(M.It.IsAny<string>(), _subject.SelectedConnection));
        }
    }
}