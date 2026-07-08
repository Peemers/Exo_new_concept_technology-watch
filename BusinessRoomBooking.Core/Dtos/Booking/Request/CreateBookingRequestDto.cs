using System.ComponentModel.DataAnnotations;

namespace BusinessRoomBooking.Core.Dtos.Booking.Request;

public record CreateBookingRequestDto
{
  [Required]
  public required DateTime StartDate { get; init; }
  [Required]
  public required DateTime EndDate { get; init; }
  [Required]
  public required int NumberOfParticipant { get; init; }
  [Required]
  public Guid RoomId { get; init; }
  public Guid WorkerId { get; init; }
}