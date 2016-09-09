using System.Collections.Generic;
using System.Threading.Tasks;
using PowerPuff.Common.Gateway;
using PowerPuff.Common.Settings;

namespace PowerPuff.Settings.Services
{
    public class ProfileGateway : ServerGatewayBase, IProfileGateway
    {
        public ProfileGateway(IApplicationSettings applicationSettings)
            : base(applicationSettings)
        {
        }

        public Task<Profile> GetProfileBy(string profileId)
        {
            return GetAsync<Profile>($"main/api/profile/{profileId}");
        }

        public Task UpdateConnection(string profileId, SocialConnection connection)
        {
            return PutAsync($"main/api/profile/{profileId}/connection/{connection.Id}", new
            {
                connection.Name,
                connection.Aliases
            });
        }
    }
}
