using System;
using Machine.Specifications;
using Moq;
using PowerPuff.Features.VideoCall.Models;
using PowerPuff.Features.VideoCall.ViewModels;
using It = Machine.Specifications.It;

namespace PowerPuff.Features.VideoCall.Tests.ViewModels
{
    public class VideoMainViewModelTests
    {
        [Subject(typeof(VideoMainViewModel))]
        public class When_any_social_connection_is_selected
        {
            Establish context = () =>
            {
                _videoCallServiceMock = new Mock<IVideoCallService>();
                _subject = new VideoMainViewModel(_videoCallServiceMock.Object);

                _selectedConnection = new SocialConnection
                {
                    SkypeId = "someskypeid",
                    Name = "some user"
                };
                _subject.SelectedConnection = _selectedConnection;
            };

            private Because of = () => _subject.CallCommand.Execute();

            private It should_invoke_video_service_with_selected_social_connection =
                () => _videoCallServiceMock.Verify(s => s.Call(_selectedConnection, Moq.It.IsAny<Action>()));

            private static Mock<IVideoCallService> _videoCallServiceMock;
            private static VideoMainViewModel _subject;
            private static SocialConnection _selectedConnection;
        }
    }
}
