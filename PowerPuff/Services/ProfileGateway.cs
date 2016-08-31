using System.Collections.Generic;
using System.Threading.Tasks;
using PowerPuff.Common.Gateway;
using PowerPuff.Common.Settings;
using PowerPuff.ViewModels;

namespace PowerPuff.Services
{
    public class ProfileGateway : ServerGatewayBase, IProfileGateway
    {
        public ProfileGateway(IApplicationSettings applicationSettings)
            : base(applicationSettings)
        {
        }

        public Task<List<SocialConnection>> GetAllSocialConnections(string profileId)
        {
            return GetAsync<List<SocialConnection>>($"api/profile/{profileId}/connections");
        }
    }
}
