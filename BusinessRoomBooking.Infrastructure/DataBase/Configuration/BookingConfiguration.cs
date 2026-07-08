using BusinessRoomBooking.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessRoomBooking.Infrastructure.DataBase.Configuration;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
  public void Configure(EntityTypeBuilder<Booking> builder)
  {
    builder.HasKey(b => b.Id);

    builder.Property(b => b.StartDate)
      .HasColumnType("datetime2")
      .IsRequired();
    
    builder.Property(b => b.EndDate)
      .HasColumnType("datetime2")
      .IsRequired();
    
    builder.HasOne(b => b.Room)
      .WithMany(r => r.Bookings)
      .HasForeignKey(b => b.RoomId)
      .OnDelete(DeleteBehavior.Restrict);
    
    builder.HasOne(b => b.Worker)
      .WithMany(w => w.Bookings)
      .HasForeignKey(b => b.WorkerId)
      .OnDelete(DeleteBehavior.Restrict);
  }
}