using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace TimeTrackingApp.Infrastructure.Extensions
{
    public static class MediatRExtensions
    {
        public static void AddMediatRDependencies(this IServiceCollection services)
        {
            services.AddMediatR(typeof(MediatRExtensions));
        }
    }
}