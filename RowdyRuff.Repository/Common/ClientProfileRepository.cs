using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RowdyRuff.Core.Common;

namespace RowdyRuff.Repository.Common
{
    public class ClientProfileRepository : IClientProfileRepository
    {
        private RowdyRuffContext _context;
        private DbSet<ClientProfile> _set;

        public ClientProfileRepository(RowdyRuffContext context)
        {
            _context = context;
            _set = _context.Set<ClientProfile>();
        }

        public List<SocialConnection> FindAllSocialConnectionsBy(string profileId)
        {
            return _context.Set<SocialConnection>()
                .Where(s => s.ClientProfileId == profileId)
                .ToList();
        }
    }
}
