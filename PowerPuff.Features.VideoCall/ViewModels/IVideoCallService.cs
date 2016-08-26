using System;
using PowerPuff.Features.VideoCall.Models;

namespace PowerPuff.Features.VideoCall.ViewModels
{
    public interface IVideoCallService
    {
        void Call(SocialConnection connection, Action finishCallback);
    }
}
