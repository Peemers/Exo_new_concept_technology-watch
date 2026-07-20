namespace BusinessRoomBooking.Domain;

public class Room
{
  public Guid Id { get; set; }
  public required string Name { get; set; }
  public required int MaxCapacity { get; set; }
  public required string Location { get; set; }
  public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
  public ICollection<RoomEquipment> RoomEquipments { get; set; } = new List<RoomEquipment>();
}