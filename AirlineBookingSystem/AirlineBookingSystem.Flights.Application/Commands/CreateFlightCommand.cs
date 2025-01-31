﻿using MediatR;

namespace AirlineBookingSystem.Flights.Application.Commands
{
    public record CreateFlightCommand(string FLightNumber,
                                      string Origin,
                                      string Destination,
                                      DateTime DepartureTime,
                                      DateTime ArrivalTime) : IRequest<Guid>;
}
