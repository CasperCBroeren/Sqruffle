using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sqruffle.Data;
using Sqruffle.Domain.Feature;

namespace Sqruffle.Application
{
    public static class ServiceConfiguration
    {
        public static void AddSqruffle(this IServiceCollection services, IConfiguration configuration, Action<IBusRegistrationConfigurator> busConfiguration, Action<IRabbitMqBusFactoryConfigurator, IBusRegistrationContext> rabbitMqbusFactory)
        { 
            services.AddHttpClient();
            services.AddLogging();
            services.AddTransient<IFeatureReactionFinder, FeatureReactionFinder>();
            services.AddMassTransit(x =>
            {
                // Configure RabbitMQ
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                   
                    rabbitMqbusFactory(cfg, context);

                });
            
                busConfiguration(x);
            });
            services.ConfigureDb(configuration);
            services.AddSingleton(TimeProvider.System);
        }  
    }
}
