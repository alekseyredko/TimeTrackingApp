using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TimeTrackingApp.Infrastructure
{
    public class IdentityContext: IdentityDbContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> options): base (options)
        {  
        }
    }
}
