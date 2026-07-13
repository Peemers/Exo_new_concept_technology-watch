using BusinessRoomBooking.Core.Dtos.Room.Queries;
using BusinessRoomBooking.Core.Dtos.Room.Summaries;
using BusinessRoomBooking.Domain;

namespace BusinessRoomBooking.Core.Interfaces.Repositories;

public interface IRoomRepository : IBaseRepository<Room>
{
  Task<IEnumerable<RoomSummaryDto>> GetAvailableRoomsAsync(AvailableRoomsQueryDto dto);
}