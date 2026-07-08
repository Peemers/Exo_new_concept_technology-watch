namespace BusinessRoomBooking.Domain;

public class Equipment
{
  public Guid Id { get; set; }
  public required string Name { get; set; }
  public ICollection<RoomEquipment> RoomEquipments { get; set; } = new List<RoomEquipment>();
}