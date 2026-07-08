using BusinessRoomBooking.Core.Dtos.Booking.Queries;
using BusinessRoomBooking.Domain;

namespace BusinessRoomBooking.Core.Interfaces.Repositories;

public interface IBookingRepository : IBaseRepository<Booking>
{
  Task<bool> HasOverlapAsync(Guid roomId, DateTime startDate, DateTime endDate, Guid? excludeBookingId = null);
  
  Task<IEnumerable<UpcomingBookingDto>> GetUpcomingBookingsByRoomAsync(Guid roomId);
}