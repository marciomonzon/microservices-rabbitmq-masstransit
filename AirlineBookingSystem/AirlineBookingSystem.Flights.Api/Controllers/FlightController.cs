using AirlineBookingSystem.Flights.Application.Commands;
using AirlineBookingSystem.Flights.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirlineBookingSystem.Flights.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public FlightController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFlights()
        {
            var flights = await _mediatr.Send(new GetAllFlightQuery());
            return Ok(flights);
        }

        [HttpPost]
        public async Task<IActionResult> AddFlight([FromBody] CreateFlightCommand command)
        {
            var id = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetAllFlights), new { id }, command);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(Guid id)
        {
            await _mediatr.Send(new DeleteFlightCommand(id));
            return NoContent();
        }
    }
}
