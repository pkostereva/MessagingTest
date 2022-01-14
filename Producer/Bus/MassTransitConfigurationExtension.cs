using EventBus;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Producer.Bus
{
    public static class MassTransitConfigurationExtension
    {
        public static IServiceCollection ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                BusExtensions.InitHost(configuration["Host"]);
                x.AddBus(context => MassTransit.Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(new Uri(configuration["Host"]), h =>
                    {
                        h.Username(configuration["Username"]);
                        h.Password(configuration["Password"]);
                    });
                }));
                
            });

            services.AddGenericRequestClient();
            services.AddMassTransitHostedService();

            return services;
        }
    }
}
