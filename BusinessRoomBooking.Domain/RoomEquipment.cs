namespace BusinessRoomBooking.Domain;

public class RoomEquipment
{
  public Guid RoomId { get; set; }
  public Room Room { get; set; } = null!;
  public Guid EquipmentId { get; set; }
  public Equipment Equipment { get; set; } = null!;
}