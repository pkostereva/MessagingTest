using EventBus.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Consumer.Bus.Consumers
{
    public class AddPersonConsumer : IConsumer<AddPerson>
    {
        private readonly ILogger<AddPersonConsumer> _logger;

        public AddPersonConsumer(ILogger<AddPersonConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<AddPerson> context)
        {
            //await _personService.AddPersonAsync(new Models.Person
            //{
            //    FirstName = context.Message.FirstName,
            //    LastName = context.Message.LastName,
            //    MiddleName = context.Message.MiddleName
            //});
            _logger.LogInformation($"{nameof(AddPerson)} was saved");
            return Task.CompletedTask;
        }
    }
}
