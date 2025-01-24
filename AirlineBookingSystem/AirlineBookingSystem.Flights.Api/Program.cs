using AirlineBookingSystem.Flights.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices();
builder.AddDatabase();
builder.AddScopedServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
