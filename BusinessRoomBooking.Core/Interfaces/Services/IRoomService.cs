using BusinessRoomBooking.Core.Dtos.Booking.Projections;
using BusinessRoomBooking.Core.Dtos.Room.Queries;
using BusinessRoomBooking.Core.Dtos.Room.Summaries;
using BusinessRoomBooking.Core.Dtos.RoomEquipment.Request;

namespace BusinessRoomBooking.Core.Interfaces.Services;

public interface IRoomService
{
  Task<IEnumerable<RoomSummaryDto>> GetAvailableRoomsAsync(AvailableRoomsQueryDto queryDto);
  
  Task AssignEquipmentToRoomAsync(Guid roomId, AssignEquipmentToRoomRequestDto requestDto);

  Task<IEnumerable<UpcomingBookingDto>> GetUpcomingBookingsByRoomAsync(Guid roomId);
}