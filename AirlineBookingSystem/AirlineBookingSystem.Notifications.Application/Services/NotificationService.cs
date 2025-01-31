using AirlineBookingSystem.Notifications.Application.Services.Interfaces;
using AirlineBookingSystem.Notifications.Core.Entities;

namespace AirlineBookingSystem.Notifications.Application.Services
{
    public class NotificationService : INotificationService
    {
        public async Task SendNotificationAsync(Notification notification)
        {
            // Simulate sending notification
            Console.WriteLine($"Notification sent to { notification.Recipient } : { notification.Message }");
        }
    }
}
