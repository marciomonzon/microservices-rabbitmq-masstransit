﻿using AirlineBookingSystem.Bookings.Core.Repositories;
using AirlineBookingSystem.Bookings.Infrastructure.Repositories;
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

        public static void AddScopedServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
        }
    }
}
