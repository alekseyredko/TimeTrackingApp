using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TimeTrackingApp.Infrastructure
{
    public class IdentityContextDesignTimeFactory : IDesignTimeDbContextFactory<IdentityContext>
    {
        public IdentityContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<IdentityContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<IdentityContext>();
            dbContextOptionsBuilder.UseSqlServer("Data Source=WAW0446;Initial Catalog=IdentityDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            return new IdentityContext(dbContextOptionsBuilder.Options);
        }
    }
}
