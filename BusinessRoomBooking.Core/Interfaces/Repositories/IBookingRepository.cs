namespace BusinessRoomBooking.Core.Interfaces.Repositories;

public interface IBookingRepository
{
  Task<bool> HasOverlapAsync(Guid roomId, DateTime startDate, DateTime endDate, Guid? excludeBookingId = null);
}