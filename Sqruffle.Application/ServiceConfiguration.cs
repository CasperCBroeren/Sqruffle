using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sqruffle.Data;

namespace Sqruffle.Application
{
    public static class ServiceConfiguration
    {
        public static void ConfigureSqruffle(this IServiceCollection services, IConfiguration configuration, Action<IBusRegistrationConfigurator> busConfiguration, Action<IRabbitMqBusFactoryConfigurator> rabbitMqbusFactory)
        {
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
                   
                    rabbitMqbusFactory(cfg);

                });
            
                busConfiguration(x);
            });
            services.ConfigureDb(configuration);
            services.AddSingleton(TimeProvider.System);
        }
    }
}
