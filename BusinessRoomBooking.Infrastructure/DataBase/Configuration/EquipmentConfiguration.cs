using BusinessRoomBooking.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessRoomBooking.Infrastructure.DataBase.Configuration;

public class EquipmentConfiguration : IEntityTypeConfiguration<Equipment>
{
  public void Configure(EntityTypeBuilder<Equipment> builder)
  {
    builder.HasKey(e => e.Id);
    
    builder.Property(e => e.Name)
      .IsRequired()
      .HasMaxLength(50);
    
    builder.HasIndex(e => e.Name)
      .IsUnique();
  }
}