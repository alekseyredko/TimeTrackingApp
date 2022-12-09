using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Playground;
using TimeTrackingApp.Core.Services;
using TimeTrackingApp.Core.Services.Interfaces;
using TimeTrackingApp.Infrastructure.Extensions;

IHostBuilder hostBuidler = Host.CreateDefaultBuilder(args);

hostBuidler.ConfigureServices((hostBuidlerContext, serviceCollection) =>
{
    serviceCollection.AddUnitOfWorkFactory(hostBuidlerContext.Configuration);
    serviceCollection.AddTransient<ITimeTrackingService, TimeTrackingService>();
    serviceCollection.AddHostedService<HostedService>();
});

await hostBuidler.RunConsoleAsync();
