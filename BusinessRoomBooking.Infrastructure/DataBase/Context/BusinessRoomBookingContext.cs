using BusinessRoomBooking.Domain;
using Microsoft.EntityFrameworkCore;

namespace BusinessRoomBooking.Infrastructure.DataBase.Context;

public class BusinessRoomBookingContext(DbContextOptions<BusinessRoomBookingContext> options) : DbContext(options)
{
  public DbSet<Booking> Bookings => Set<Booking>();
  public DbSet<Equipment> Equipments => Set<Equipment>();
  public DbSet<Room> Rooms => Set<Room>();
  public DbSet<RoomEquipment> RoomEquipments => Set<RoomEquipment>();
  public DbSet<Worker> Workers => Set<Worker>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(BusinessRoomBookingContext).Assembly);
  }
}