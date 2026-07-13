using System.ComponentModel.DataAnnotations;

namespace BusinessRoomBooking.Core.Dtos.RoomEquipment.Request;

public record AssignEquipmentToRoomRequestDto
{
  [Required]
  public required Guid EquipmentId { get; init; }
}