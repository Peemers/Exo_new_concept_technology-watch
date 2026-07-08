using BusinessRoomBooking.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessRoomBooking.Infrastructure.DataBase.Configuration;

public class WorkerConfiguration : IEntityTypeConfiguration<Worker>
{
  public void Configure(EntityTypeBuilder<Worker> builder)
  {
    builder.HasKey(w => w.Id);
    
    builder.Property(w => w.FirstName)
      .IsRequired()
      .HasMaxLength(50);
    
    builder.Property(w => w.LastName)
      .IsRequired()
      .HasMaxLength(75);

    builder.Property(w => w.Email)
      .IsRequired()
      .HasMaxLength(256);
    
    builder.HasIndex(w => w.Email)
      .IsUnique();
  }
}