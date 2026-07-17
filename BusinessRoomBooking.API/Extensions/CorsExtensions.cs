namespace BusinessRoomBooking.Extensions;

public static class CorsExtensions
{
  public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
  {
    services.AddCors(options =>
    {
      options.AddPolicy("AllowAngularDev", policy =>
      {
        policy.WithOrigins("http://localhost:4200")
          .AllowAnyMethod()
          .AllowAnyHeader();
      });
    });

    return services;
  }
}