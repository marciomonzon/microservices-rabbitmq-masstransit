using AirlineBookingSystem.Notifications.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices();
builder.AddDatabase();
builder.AddScopedServices();
builder.AddMediatr();
builder.AddMassTransit();

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
