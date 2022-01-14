using EventBus;
using EventBus.Models;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Producer.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Producer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IBus _bus;

        public PersonController(IBus bus, ILogger<PersonController> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PublishHistoryAsync(PersonRequest request, CancellationToken ct)
        {
            await _bus.SendAsync(new AddPerson { LastName = request.LastName, FirstName = request.FirstName, MiddleName = request.MiddleName }, QueueNames.TestQueue);
            return Accepted();
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetPersonAsync(int userId, CancellationToken ct)
        {
            var response = await _bus.GetResponseAsync<GetPerson, PersonResponse>(new GetPerson(userId), QueueNames.TestRequestQueue);
              _logger.LogInformation("Found person with UserId: {id}", userId);
            return Ok(response);
        }
    }
}
