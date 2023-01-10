using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeTrackingApp.Application.UnitOfWork;
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

        public static void AddInMemoryUnitOfWorkFactory(this IServiceCollection services, IConfiguration configuration, string connectionStringPath = "ApplicationDbContext")
        {
            services.AddDbContextFactory<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("TimeTrackingDb");
            });
            services.AddTransient<IUnitOfWorkFactory, UnitOfWorkFactory>();                            
        }
    }
}
