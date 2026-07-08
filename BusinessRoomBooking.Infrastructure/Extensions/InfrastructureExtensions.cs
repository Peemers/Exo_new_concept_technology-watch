using BusinessRoomBooking.Infrastructure.DataBase.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessRoomBooking.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    //DbContext
    services.AddDbContext<BusinessRoomBookingContext>(options =>
      options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    
    return services;
  }
}