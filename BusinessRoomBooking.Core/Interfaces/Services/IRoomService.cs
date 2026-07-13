using BusinessRoomBooking.Core.Dtos.Room.Queries;
using BusinessRoomBooking.Core.Dtos.Room.Summaries;
using BusinessRoomBooking.Core.Dtos.RoomEquipment.Request;

namespace BusinessRoomBooking.Core.Interfaces.Services;

public interface IRoomService
{
  Task<IEnumerable<RoomSummaryDto>> GetAvailableRoomsAsync(AvailableRoomsQueryDto queryDto);
  
  Task AssignEquipmentToRoomAsync(Guid roomId, AssignEquipmentToRoomRequestDto requestDto);
}