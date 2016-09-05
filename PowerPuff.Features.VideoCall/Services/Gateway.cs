using System.Collections.Generic;
using System.Threading.Tasks;
using PowerPuff.Common.Gateway;
using PowerPuff.Common.Settings;
using PowerPuff.Features.VideoCall.Models;
using PowerPuff.Features.VideoCall.ViewModels;

namespace PowerPuff.Features.VideoCall.Services
{
    public class Gateway : ServerGatewayBase, IGateway
    {
        public Gateway(IApplicationSettings applicationSettings) : 
            base(applicationSettings)
        {
        }

        public Task<List<SocialConnection>> GetSocialConnections(string profileId)
        {
            return GetAsync<List<SocialConnection>>($"video/api/{profileId}/settings");
        }
    }
}
