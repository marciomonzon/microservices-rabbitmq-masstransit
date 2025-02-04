using AirlineBookingSystem.BuildingBlocks.Contracts.EventBus.Messages;
using AirlineBookingSystem.Notifications.Application.Commands;
using MassTransit;
using MediatR;

namespace AirlineBookingSystem.Notifications.Application.Consumers
{
    public class PaymentProcessConsumer : IConsumer<PaymentProcessedEvent>
    {
        private readonly IMediator _mediator;

        public PaymentProcessConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<PaymentProcessedEvent> context)
        {
            var paymentProcessedEvent = context.Message;
            var message = $"Payment of {paymentProcessedEvent.Amount} for Booking Id: {paymentProcessedEvent.BookingId} " +
                $"was processed successfully";

            var command = new SendNotificationCommand("test@test.com", message, "Email");
            await _mediator.Send(command);
        }
    }
}
