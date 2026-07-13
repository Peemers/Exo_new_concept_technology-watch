using BusinessRoomBooking.Domain;

namespace BusinessRoomBooking.Core.Interfaces.Repositories;

public interface IRoomEquipmentRepository : IBaseRepository<RoomEquipment>
{
  Task<bool> EquipmentAlreadyExistInRoomAsync(Guid roomId, Guid equipmentId);
}