using Consumer.Bus.Consumers;
using EventBus;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Consumer.Bus
{
    public static class MassTransitConfigurationExtension
    {
        public static IServiceCollection ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<GetPersonConsumer>();
                x.AddConsumer<AddPersonConsumer>();

                x.AddBus(context => MassTransit.Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(new Uri(configuration["Host"]), h =>
                    {
                        h.Username(configuration["Username"]);
                        h.Password(configuration["Password"]);
                    });

                    cfg.DefaultReceiveEndpoint<AddPersonConsumer>(QueueNames.TestQueue, context);
                    cfg.DefaultReceiveEndpoint<GetPersonConsumer>(QueueNames.TestRequestQueue, context);
                }));
            });

            services.AddMassTransitHostedService();

            return services;
        }

        private static void DefaultReceiveEndpoint<T>(
            this IRabbitMqBusFactoryConfigurator configuration, string queueName, IRegistration context)
        where T : class, IConsumer
        {
            configuration.ReceiveEndpoint(queueName, ep =>
            {
                ep.PrefetchCount = 16;
                ep.UseMessageRetry(r => r.Interval(2, 100));
                ep.ConfigureConsumer<T>(context);
            });
        }
    }
}
