using AirlineBookingSystem.Flights.Application.Handlers;
using AirlineBookingSystem.Flights.Core.Repositories;
using AirlineBookingSystem.Flights.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace AirlineBookingSystem.Flights.Api.Extensions
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
            builder.Services.AddScoped<IFlightRepository, FlightRepository>();
        }

        public static void AddMediatr(this WebApplicationBuilder builder)
        {
            var assemblies = new Assembly[]
            {
                Assembly.GetExecutingAssembly(),
                typeof(CreateFlightHandler).Assembly,
                typeof(DeleteFlightHandler).Assembly,
                typeof(GetAllFlightsHandler).Assembly
            };

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
        }
    }
}
