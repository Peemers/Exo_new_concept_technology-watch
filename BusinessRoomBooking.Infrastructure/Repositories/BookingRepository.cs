using BusinessRoomBooking.Core.Interfaces.Repositories;
using BusinessRoomBooking.Domain;
using BusinessRoomBooking.Infrastructure.DataBase.Context;
using Microsoft.EntityFrameworkCore;

namespace BusinessRoomBooking.Infrastructure.Repositories;

public class BookingRepository(BusinessRoomBookingContext context) 
  : BaseRepository<Booking>(context), IBookingRepository
{
  public async Task<bool> HasOverlapAsync(Guid roomId, DateTime startDate, DateTime endDate, Guid? excludeBookingId = null)
  {
    IQueryable<Booking> query = DbSet.Where(b =>
      b.RoomId == roomId &&
      b.StartDate < endDate &&
      b.EndDate > startDate);
    if (excludeBookingId.HasValue)
    {
      query = query.Where(b => b.Id != excludeBookingId.Value);
    }

    return await query.AnyAsync();
  }
}