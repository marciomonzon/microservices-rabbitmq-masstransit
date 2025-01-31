﻿using AirlineBookingSystem.Flights.Core.Entities;
using MediatR;

namespace AirlineBookingSystem.Flights.Application.Queries
{
    public record GetAllFlightQuery : IRequest<IEnumerable<Flight>>;
}
