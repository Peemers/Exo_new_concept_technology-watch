using BusinessRoomBooking.Core.Dtos.RoomEquipment.Request;
using BusinessRoomBooking.Domain;

namespace BusinessRoomBooking.Core.Mappers;

public static class RoomEquipmentMapper
{
  public static RoomEquipment ToRoomEquipment(this AssignEquipmentToRoomRequestDto dto, Guid roomId)
  {
    return new RoomEquipment
    {
      RoomId = roomId,
      EquipmentId = dto.EquipmentId,
    };
  }
}