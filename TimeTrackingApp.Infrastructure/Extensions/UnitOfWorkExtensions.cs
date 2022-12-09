using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeTrackingApp.Core.UnitOfWork;
using TimeTrackingApp.Infrastructure.UnitOfWork;

namespace TimeTrackingApp.Infrastructure.Extensions
{
    public static class UnitOfWorkExtensions
    {
        public static void AddUnitOfWorkFactory(this IServiceCollection services, IConfiguration configuration, string connectionStringPath = "ApplicationDbContext")
        {
            services.AddDbContextFactory<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(connectionStringPath));
            });
            services.AddTransient<IUnitOfWorkFactory, UnitOfWorkFactory>();                            
        }
    }
}
