using System.Collections.Generic;
using System.Threading.Tasks;

namespace PowerPuff.Settings
{
    public interface IProfileGateway
    {
        Task<List<SocialConnection>> GetAllSocialConnections(string profileId);
    }
}
