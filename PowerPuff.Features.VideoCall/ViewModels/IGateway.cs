using System.Collections.Generic;
using System.Threading.Tasks;
using PowerPuff.Features.VideoCall.Models;

namespace PowerPuff.Features.VideoCall.ViewModels
{
    public interface IGateway
    {
        Task<List<SocialConnection>> GetSocialConnections(string profileId);
    }
}
