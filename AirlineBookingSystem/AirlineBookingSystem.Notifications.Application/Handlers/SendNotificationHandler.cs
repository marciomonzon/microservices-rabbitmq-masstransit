using AirlineBookingSystem.Notifications.Application.Commands;
using AirlineBookingSystem.Notifications.Application.Services.Interfaces;
using AirlineBookingSystem.Notifications.Core.Entities;
using MediatR;

namespace AirlineBookingSystem.Notifications.Application.Handlers
{
    public class SendNotificationHandler : IRequestHandler<SendNotificationCommand>
    {
        private readonly INotificationService _notificationService;

        public SendNotificationHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Handle(SendNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                Message = request.Message,
                Recipient = request.Recipient,
                SentAt = DateTime.UtcNow,
                Type = request.Type,
            };

            await _notificationService.SendNotificationAsync(notification);
        }
    }
}
