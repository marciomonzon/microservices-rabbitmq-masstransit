using AirlineBookingSystem.Flights.Application.Commands;
using AirlineBookingSystem.Flights.Core.Entities;
using AirlineBookingSystem.Flights.Core.Repositories;
using MediatR;

namespace AirlineBookingSystem.Flights.Application.Handlers
{
    public class CreateFlightHandler : IRequestHandler<CreateFlightCommand, Guid>
    {
        private readonly IFlightRepository _flightRepository;

        public CreateFlightHandler(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task<Guid> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
        {
            var flight = new Flight
            {
                Id = Guid.NewGuid(),
                FlightNumber = request.FLightNumber,
                Origin = request.Origin,
                ArrivalTime = request.ArrivalTime,
                DepartureTime = request.DepartureTime,
                Destination = request.Destination
            };

            await _flightRepository.AddFlightAsync(flight);
            return flight.Id;
        }
    }
}
