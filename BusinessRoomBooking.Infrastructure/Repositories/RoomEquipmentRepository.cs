using BusinessRoomBooking.Core.Interfaces.Repositories;
using BusinessRoomBooking.Domain;
using BusinessRoomBooking.Infrastructure.DataBase.Context;
using Microsoft.EntityFrameworkCore;

namespace BusinessRoomBooking.Infrastructure.Repositories;

public class RoomEquipmentRepository(BusinessRoomBookingContext context)
  : BaseRepository<RoomEquipment>(context), IRoomEquipmentRepository
{
  public async Task<bool> EquipmentAlreadyExistInRoomAsync(Guid roomId, Guid equipmentId)
  {
    IQueryable<RoomEquipment> query = DbSet.Where(re =>
      re.RoomId == roomId &&
      re.EquipmentId == equipmentId);
    return await query.AnyAsync();
  }
}