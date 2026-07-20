using BusinessRoomBooking.Core.Dtos.Room.Request;
using BusinessRoomBooking.Core.Dtos.Room.Response;
using BusinessRoomBooking.Domain;

namespace BusinessRoomBooking.Core.Mappers;

public static class RoomMapper
{
  public static RoomResponseDto ToRoomResponseDto(this Room room)
  {
    return new RoomResponseDto
    {
      Id = room.Id,
      MaxCapacity = room.MaxCapacity,
      Location = room.Location,
    };
  }

  public static Room ToRoom(this CreateRoomRequestDto dto)
  {
    return new Room
    {
      Id = Guid.NewGuid(),
      MaxCapacity = dto.MaxCapacity,
      Location = dto.Location,
    };
  }
}