using System.Collections.Generic;

namespace RowdyRuff.Core.Common
{
    public interface IClientProfileRepository
    {
        List<SocialConnection> FindAllSocialConnectionsBy(string profileId);
    }
}
