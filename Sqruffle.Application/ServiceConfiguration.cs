using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sqruffle.Data;
using Sqruffle.Domain.Feature;
using Sqruffle.Utilities.Configuration;

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
                var sqruffleConfiguration = configuration.GetSection(nameof(SqruffleConfiguration)).Get<SqruffleConfiguration>();
               
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(sqruffleConfiguration!.Host, "/", h =>
                    {
                        h.Username(sqruffleConfiguration!.UserName);
                        h.Password(sqruffleConfiguration!.Password);
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
