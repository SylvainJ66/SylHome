using Com.SylHome.Adapters.Secondary.CallForFunds;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SylHome.Hexagon.Gateways.Repositories;

CreateHostBuilder(args).Build().Run();


IHostBuilder CreateHostBuilder(string[] args)
{
    var hostBuilder = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, builder) =>
        {
            builder.SetBasePath(Directory.GetCurrentDirectory());
        })
        .ConfigureServices((context, services) =>
        {
            services.AddSingleton<ICallForFundsRepository, DapperCallForFundsRepository>();
        });

    return hostBuilder;
}
