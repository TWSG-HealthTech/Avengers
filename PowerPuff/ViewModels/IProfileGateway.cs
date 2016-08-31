using System.Collections.Generic;
using System.Threading.Tasks;

namespace PowerPuff.ViewModels
{
    public interface IProfileGateway
    {
        Task<List<SocialConnection>> GetAllSocialConnections(string profileId);
    }
}
