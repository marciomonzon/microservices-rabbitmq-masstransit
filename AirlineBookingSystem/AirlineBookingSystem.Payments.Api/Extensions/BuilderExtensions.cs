using AirlineBookingSystem.BuildingBlocks.Common;
using AirlineBookingSystem.BuildingBlocks.Contracts.EventBus.Messages;
using AirlineBookingSystem.Payments.Application.Consumers;
using AirlineBookingSystem.Payments.Application.Handlers;
using AirlineBookingSystem.Payments.Core.Repositories;
using AirlineBookingSystem.Payments.Infrastructure.Repositories;
using MassTransit;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace AirlineBookingSystem.Payments.Api.Extensions
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
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
        }

        public static void AddMediatr(this WebApplicationBuilder builder)
        {
            var assemblies = new Assembly[]
            {
                Assembly.GetExecutingAssembly(),
                typeof(ProcessPaymentHandler).Assembly,
                typeof(RefundPaymentHandler).Assembly
            };

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
        }

        public static void AddMassTransit(this WebApplicationBuilder builder)
        {
            builder.Services.AddMassTransit((config) =>
            {
                config.AddConsumer<FlightBookedConsumer>();

                config.UsingRabbitMq((context, config) =>
                {
                    config.Host(builder.Configuration["EventBusSettings:HostAddress"]);
                    config.ReceiveEndpoint(EventBusConstant.FlightBookedQueue, c =>
                    {
                        c.ConfigureConsumer<FlightBookedConsumer>(context);
                    });
                });
            });
        }
    }
}
