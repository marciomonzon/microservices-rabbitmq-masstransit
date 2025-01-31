using AirlineBookingSystem.Notifications.Core.Entities;

namespace AirlineBookingSystem.Notifications.Application.Services.Interfaces
{
    public interface INotificationService
    {
        Task SendNotificationAsync(Notification notification);
    }
}
