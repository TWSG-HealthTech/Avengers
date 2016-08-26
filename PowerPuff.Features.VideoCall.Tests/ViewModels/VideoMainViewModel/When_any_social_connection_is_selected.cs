using System;
using Machine.Specifications;
using PowerPuff.Features.VideoCall.Models;
using Prism.Regions;
using SUT = PowerPuff.Features.VideoCall.ViewModels;
using M = Moq;

namespace PowerPuff.Features.VideoCall.Tests.ViewModels.VideoMainViewModel
{
    [Subject(typeof(SUT.VideoMainViewModel))]
    public class When_any_social_connection_is_selected
    {
        Establish context = () =>
        {
            var regionManagerMock = new M.Mock<IRegionManager>();
            _videoCallServiceMock = new M.Mock<SUT.IVideoCallService>();
            _subject = new SUT.VideoMainViewModel(regionManagerMock.Object, _videoCallServiceMock.Object);

            _selectedConnection = new SocialConnection
            {
                SkypeId = "someskypeid",
                Name = "some user"
            };
            _subject.SelectedConnection = _selectedConnection;
        };

        private Because of = () => _subject.CallCommand.Execute();

        private It should_invoke_video_service_with_selected_social_connection =
            () => _videoCallServiceMock.Verify(s => s.Call(_selectedConnection, M.It.IsAny<Action>()));

        private static M.Mock<SUT.IVideoCallService> _videoCallServiceMock;
        private static SUT.VideoMainViewModel _subject;
        private static SocialConnection _selectedConnection;
    }
}
