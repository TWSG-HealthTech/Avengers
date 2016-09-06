using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Machine.Specifications;
using PowerPuff.Features.VideoCall.Models;
using PowerPuff.Features.VideoCall.ViewModels;
using Prism.Regions;
using M = Moq;

namespace PowerPuff.Features.VideoCall.Tests.ViewModels
{
    [Subject(typeof(SettingsViewModel))]
    public class SettingsViewModelTests
    {
        Establish context = () =>
        {
            _gatewayMock = new M.Mock<IGateway>();
            _subject = new SettingsViewModel(_gatewayMock.Object);
        };

        private static M.Mock<IGateway> _gatewayMock;
        private static SettingsViewModel _subject;

        public class When_navigate_to_settings_view
        {
            Establish context = () =>
            {
                _connection = new SocialConnection
                {
                    Id = 1,
                    Name = "somename",
                    Skype = "someskypeid"
                };
                _gatewayMock.Setup(g => g.GetSocialConnections(M.It.IsAny<string>()))
                    .Returns(Task.FromResult(new List<SocialConnection> {_connection}));
            };

            private Because of = NavigateTo;

            private It should_load_settings_from_server = () =>
            {
                _subject.Connections.Count.ShouldEqual(1);

                var actual = _subject.Connections.First();
                actual.Id.ShouldEqual(1);
                actual.Name.ShouldEqual("somename");
                actual.Skype.ShouldEqual("someskypeid");
            };

            private static SocialConnection _connection;

            private static void NavigateTo()
            {
                var regionNavigationServiceMock = new M.Mock<IRegionNavigationService>();
                var context = new NavigationContext(regionNavigationServiceMock.Object, new Uri("some uri", UriKind.RelativeOrAbsolute));
                _subject.OnNavigatedTo(context);
            }
        }

        public class When_update_button_is_clicked
        {
            private Establish context = () =>
            {
                _connection = new SocialConnection
                {
                    Id = 1,
                    Name = "somename",
                    Skype = "someskypeid"
                };

                _subject.SelectedConnection = _connection;
            };

            private Because of = () => _subject.UpdateSkypeCommand.Execute();

            private It should_invoke_update_from_server = () =>
            {
                _gatewayMock.Verify(g => g.Update(M.It.IsAny<string>(), _connection));
            };

            private static SocialConnection _connection;
        }
    }
}