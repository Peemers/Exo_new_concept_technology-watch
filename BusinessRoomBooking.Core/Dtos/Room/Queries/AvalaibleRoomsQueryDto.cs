using System.ComponentModel.DataAnnotations;

namespace BusinessRoomBooking.Core.Dtos.Room.Queries;

public record AvailableRoomsQueryDto
{
  [Required]
  public required DateTime StartDate { get; init; }
  [Required]
  public required DateTime EndDate { get; init; }
  [Range(1, 200, 
    ErrorMessage = "Minimum 1 participant, maximum 200 participants")]
  public required int MinCapacity { get; init; }
}