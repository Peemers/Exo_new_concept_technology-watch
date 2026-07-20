using BusinessRoomBooking.Core.Dtos.Room.Queries;
using BusinessRoomBooking.Core.Dtos.Room.Summaries;
using BusinessRoomBooking.Core.Interfaces.Repositories;
using BusinessRoomBooking.Domain;
using BusinessRoomBooking.Infrastructure.DataBase.Context;
using Microsoft.EntityFrameworkCore;

namespace BusinessRoomBooking.Infrastructure.Repositories;

public class RoomRepository(BusinessRoomBookingContext context)
  : BaseRepository<Room>(context), IRoomRepository
  
{
  public async Task<IEnumerable<RoomSummaryDto>> GetAvailableRoomsAsync(AvailableRoomsQueryDto dto)
  {
    return await DbSet
      .Where(r => r.MaxCapacity >= dto.MinCapacity
                  && r.Bookings.All(b => b.EndDate <= dto.StartDate || b.StartDate >= dto.EndDate))
      .Select(r => new RoomSummaryDto
      {
        Id = r.Id,
        Location = r.Location,
        Name = r.Name,
      }).ToListAsync();
  }
}