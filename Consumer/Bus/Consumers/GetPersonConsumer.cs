using EventBus.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Consumer.Bus.Consumers
{
    public class GetPersonConsumer : IConsumer<GetPerson>
    {
        private ILogger<GetPersonConsumer> _logger;

        public GetPersonConsumer(ILogger<GetPersonConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<GetPerson> context)
        {
            await context.RespondAsync<PersonResponse>(new 
            {
                UserId = 1,
                LastName = "К",
                FirstName = "P",
                MiddleName = "A",
                Status = "Active",
                CreatedAt = DateTime.Now
            });
            _logger.LogInformation("Created new person!");
        }
    }
}
