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
            return GetAsync<Profile>($"api/profile/{profileId}");
        }
    }
}
