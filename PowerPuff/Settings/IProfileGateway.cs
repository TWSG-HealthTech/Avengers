using System.Collections.Generic;
using System.Threading.Tasks;

namespace PowerPuff.Settings
{
    public interface IProfileGateway
    {
        Task<Profile> GetProfileBy(string profileId);
    }
}
