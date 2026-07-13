using BusinessRoomBooking.Core.Dtos.Room.Queries;
using BusinessRoomBooking.Core.Dtos.Room.Summaries;
using BusinessRoomBooking.Core.Interfaces.Repositories;
using BusinessRoomBooking.Core.Interfaces.Services;
using BusinessRoomBooking.Domain;

namespace BusinessRoomBooking.Core.Services;

public class RoomService(IRoomRepository roomRepository) : IRoomService
{
  public async Task<IEnumerable<RoomSummaryDto>> GetAvailableRoomsAsync(AvailableRoomsQueryDto queryDto)
  {
    IEnumerable<RoomSummaryDto> rooms = await roomRepository.GetAvailableRoomsAsync(queryDto);
    return rooms;
  }
}