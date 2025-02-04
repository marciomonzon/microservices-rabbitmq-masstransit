using AirlineBookingSystem.BuildingBlocks.Common;
using AirlineBookingSystem.Notifications.Application.Consumers;
using AirlineBookingSystem.Notifications.Application.Handlers;
using AirlineBookingSystem.Notifications.Application.Services;
using AirlineBookingSystem.Notifications.Application.Services.Interfaces;
using AirlineBookingSystem.Notifications.Core.Repositories;
using AirlineBookingSystem.Notifications.Infrastructure.Repositories;
using MassTransit;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace AirlineBookingSystem.Notifications.Api.Extensions
{
    public static class BuilderExtensions
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }

        public static void AddDatabase(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IDbConnection>(sp =>
                    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
        }

        public static void AddScopedServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
        }

        public static void AddMediatr(this WebApplicationBuilder builder)
        {
            var assemblies = new Assembly[]
            {
                Assembly.GetExecutingAssembly(),
                typeof(SendNotificationHandler).Assembly
            };

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
        }

        public static void AddMassTransit(this WebApplicationBuilder builder)
        {
            builder.Services.AddMassTransit((config) =>
            {
                config.AddConsumer<PaymentProcessConsumer>();

                config.UsingRabbitMq((context, config) =>
                {
                    config.Host(builder.Configuration["EventBusSettings:HostAddress"]);
                    config.ReceiveEndpoint(EventBusConstant.PaymentProcessedQueue, c =>
                    {
                        c.ConfigureConsumer<PaymentProcessConsumer>(context);
                    });
                });
            });
        }
    }
}
