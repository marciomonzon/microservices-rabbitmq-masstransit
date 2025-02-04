using AirlineBookingSystem.Bookings.Application.Consumers;
using AirlineBookingSystem.Bookings.Application.Handlers;
using AirlineBookingSystem.Bookings.Core.Repositories;
using AirlineBookingSystem.Bookings.Infrastructure.Repositories;
using AirlineBookingSystem.BuildingBlocks.Common;
using MassTransit;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace AirlineBookingSystem.Bookings.Api.Extensions
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
            builder.Services
                   .AddScoped<IDbConnection>(sp =>
                   new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
        }

        public static void AddScopedServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
        }

        public static void AddMediatr(this WebApplicationBuilder builder)
        {
            var assemblies = new Assembly[]
            {
                Assembly.GetExecutingAssembly(),
                typeof(CreateBookingHandler).Assembly,
                typeof(GetBookingHandler).Assembly
            };

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
        }

        public static void AddMassTransit(this WebApplicationBuilder builder)
        {
            builder.Services.AddMassTransit((config) =>
            {
                config.AddConsumer<NotificationEventConsumer>();

                config.UsingRabbitMq((context, config) =>
                {
                    config.Host(builder.Configuration["EventBusSettings:HostAddress"]);
                    config.ReceiveEndpoint(EventBusConstant.NotificationSentQueue, c =>
                    {
                        c.ConfigureConsumer<NotificationEventConsumer>(context);
                    });
                });
            });
        }
    }
}
