using System.Collections.Generic;
using RowdyRuff.Core.Common;

namespace RowdyRuff.Repository
{
    public static class DbInitializer
    {
        public static void Seed(RowdyRuffContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var sampleProfile = FakeProfile();
            context.Set<ClientProfile>().Add(sampleProfile);
            
            context.SaveChanges();
        }

        private static ClientProfile FakeProfile()
        {
            var profile = new ClientProfile(
                "a111222a", 
                "rowdyruff@thoughtworks.com");

            profile.Connections.Add(new SocialConnection("someskypeid1", new List<string> { "son" }));
            profile.Connections.Add(new SocialConnection("someskypeid2", new List<string> { "daughter" }));

            return profile;
        }
    }
}
