namespace BusinessRoomBooking.Domain;

public class Worker
{
  public Guid Id { get; set; }
  public required string FirstName { get; set; }
  public required string LastName { get; set; }
  public required string Email { get; set; }
  public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}