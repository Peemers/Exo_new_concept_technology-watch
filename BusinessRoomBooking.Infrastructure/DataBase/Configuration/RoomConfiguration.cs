using BusinessRoomBooking.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessRoomBooking.Infrastructure.DataBase.Configuration;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
  public void Configure(EntityTypeBuilder<Room> builder)
  {
    builder.HasKey(r => r.Id);

    builder.Property(r => r.Name)
      .IsRequired()
      .HasMaxLength(100);
    
    builder.Property(r => r.Location)
      .IsRequired()
      .HasMaxLength(200);
    
    builder.Property(r => r.MaxCapacity)
      .IsRequired();
  }
}