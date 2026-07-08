namespace BusinessRoomBooking.Core.Dtos.Booking.Queries;

public class UpcomingBookingDto
{
  public DateTime StartDate { get; set; }
  public DateTime EndDate { get; set; }
  public string WorkerFirstName { get; set; } = null!;
  public string WorkerLastName { get; set; } = null!;
}