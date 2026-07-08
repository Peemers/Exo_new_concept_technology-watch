namespace BusinessRoomBooking.Domain;

public class Booking
{
  public Guid Id { get; set; }
  public required DateTime StartDate { get; set; }
  public required DateTime EndDate { get; set; }
  public required int NumberOfParticipant { get; set; }
  public Guid RoomId { get; set; }
  public Room Room { get; set; } = null!;
  public Guid WorkerId { get; set; }
  public Worker Worker { get; set; } = null!;
}