using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TimeTrackingApp.Infrastructure.Behaviours;

namespace TimeTrackingApp.Infrastructure.Extensions
{
    public static class MediatRExtensions
    {
        public static void AddMediatRDependencies(this IServiceCollection services)
        {
            services.AddMediatR(typeof(MediatRExtensions));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));           
        }
    }
}