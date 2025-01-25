using MediatR;

namespace AirlineBookingSystem.Bookings.Application.Commands
{
    public record CreateBookingCommand(Guid id, Guid FlightId, string PassengerName, string SeatNumber) : IRequest<Guid>;
}
