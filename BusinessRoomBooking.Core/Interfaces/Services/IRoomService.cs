using BusinessRoomBooking.Core.Dtos.Room.Queries;
using BusinessRoomBooking.Core.Dtos.Room.Summaries;

namespace BusinessRoomBooking.Core.Interfaces.Services;

public interface IRoomService
{
  Task<IEnumerable<RoomSummaryDto>> GetAvailableRoomsAsync(AvailableRoomsQueryDto queryDto);
}