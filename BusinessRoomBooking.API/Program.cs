using BusinessRoomBooking.Core.Extensions;
using BusinessRoomBooking.Extensions;
using BusinessRoomBooking.Infrastructure.Extensions;
using BusinessRoomBooking.MiddleWare;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddCorsConfiguration();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularDev");

app.MapControllers();
app.Run();
