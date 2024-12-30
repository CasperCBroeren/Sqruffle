using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sqruffle.Application.CommandHandlers.Product;
using Sqruffle.Application.EventListeners.Product;
using Sqruffle.Data;
using Sqruffle.Domain.Feature;

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
                services.AddMassTransit(x =>
                {
                    // Register the consumer
                    x.AddConsumer<AddProductConsumer>();
                    x.AddConsumer<ProductCreatedListener>();

                    // Configure RabbitMQ
                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host("localhost", "/", h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        });

                        cfg.ReceiveEndpoint("ProductCreated_service_queue", e =>
                        {
                            e.ConfigureConsumer<ProductCreatedListener>(context);
                        });

                        cfg.ConfigureEndpoints(context);
                    });
                });

                services.AddTransient<IFeatureReactionFinder, FeatureReactionFinder>();
                services.AddHttpClient();
                services.AddLogging();
                services.ConfigureDb(hostContext.Configuration);
            });
}
