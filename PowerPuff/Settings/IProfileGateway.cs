using System.Threading.Tasks;

namespace PowerPuff.Settings
{
    public interface IProfileGateway
    {
        Task<Profile> GetProfileBy(string profileId);
        Task UpdateConnection(string profileId, SocialConnection connection);
    }
}
