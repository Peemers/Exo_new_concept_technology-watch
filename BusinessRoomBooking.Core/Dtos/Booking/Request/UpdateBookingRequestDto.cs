using System.ComponentModel.DataAnnotations;

namespace BusinessRoomBooking.Core.Dtos.Booking.Request;

public record UpdateBookingRequestDto
{
  [Required]
  public required DateTime StartDate { get; init; }
  [Required]
  public required DateTime EndDate { get; init; }
  [Range(1, 200, ErrorMessage = "Minimum 1 participant, maximum 200 participants")]
  public required int NumberOfParticipant { get; init; }
}