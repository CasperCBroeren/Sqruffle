using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sqruffle.Application;
using Sqruffle.Application.Products.CommandHandlers;
using System.Reflection;

namespace Sqruffle.Service;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration(cfg =>
        cfg.AddUserSecrets<Program>())
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<BackgroundTimer>();
                services.AddSqruffle(hostContext.Configuration, x =>
                {
                    x.AddConsumer<AddProductConsumer>();
                    x.AddConsumer<ProductCreatedEventConsumerListener>();
                    x.AddConsumer<DailyCheckEventConsumerListener>(); 
                },
                (rb, rbContext) =>
                {
                    rb.ReceiveEndpoint("ProductCreated_service_queue", e =>
                    {
                        e.ConfigureConsumer<ProductCreatedEventConsumerListener>(rbContext);
                    });
                    rb.ConfigureEndpoints(rbContext);
                });
            });
}
