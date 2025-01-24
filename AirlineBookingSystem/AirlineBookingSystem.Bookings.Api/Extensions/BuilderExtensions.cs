using Microsoft.Data.SqlClient;
using System.Data;

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
            builder.Services.AddScoped<IDbConnection>(sp =>
                    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
        }

        public static void AddMediatrCommands(this WebApplicationBuilder builder)
        {
            //builder.Services.AddMediatR(config =>
            //config.RegisterServicesFromAssemblies(typeof(CreateUserCommand).Assembly));
        }

        public static void AddScopedServices(this WebApplicationBuilder builder)
        {
        }
    }
}
