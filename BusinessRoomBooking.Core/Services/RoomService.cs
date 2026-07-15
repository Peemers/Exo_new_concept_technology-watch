using BusinessRoomBooking.Core.Dtos.Booking.Projections;
using BusinessRoomBooking.Core.Dtos.Booking.Response;
using BusinessRoomBooking.Core.Dtos.Room.Queries;
using BusinessRoomBooking.Core.Dtos.Room.Summaries;
using BusinessRoomBooking.Core.Dtos.RoomEquipment.Request;
using BusinessRoomBooking.Core.Exceptions.EquipmentExceptions;
using BusinessRoomBooking.Core.Exceptions.RoomExceptions;
using BusinessRoomBooking.Core.Interfaces.Repositories;
using BusinessRoomBooking.Core.Interfaces.Services;
using BusinessRoomBooking.Core.Mappers;
using BusinessRoomBooking.Domain;

namespace BusinessRoomBooking.Core.Services;

public class RoomService(
  IRoomRepository roomRepository,
  IRoomEquipmentRepository roomEquipmentRepository,
  IBaseRepository<Equipment> equipmentRepository,
  IBookingRepository bookingRepository) : IRoomService
{
  public async Task<IEnumerable<RoomSummaryDto>> GetAvailableRoomsAsync(AvailableRoomsQueryDto queryDto)
  {
    IEnumerable<RoomSummaryDto> rooms = await roomRepository.GetAvailableRoomsAsync(queryDto);
    return rooms;
  }

  public async Task AssignEquipmentToRoomAsync(Guid roomId, AssignEquipmentToRoomRequestDto dto)
  {
    Room? room = await roomRepository.GetByIdAsync(roomId);
    if (room is null) throw new RoomNotFoundException(roomId);

    Equipment? equipment = await equipmentRepository.GetByIdAsync(dto.EquipmentId);
    if (equipment is null) throw new EquipmentNotFoundException(dto.EquipmentId);

    bool exist = await roomEquipmentRepository.EquipmentAlreadyExistInRoomAsync(roomId, dto.EquipmentId);
    if (exist) throw new EquipmentAlreadyAssignedException(roomId, dto.EquipmentId);

    RoomEquipment roomEquipment = dto.ToRoomEquipment(roomId);

    await roomEquipmentRepository.AddAsync(roomEquipment);
    await roomEquipmentRepository.SaveChangesAsync();
  }

  public async Task<IEnumerable<UpcomingBookingDto>> GetUpcomingBookingsByRoomAsync(Guid roomId)
  {
    IEnumerable<UpcomingBookingDto> bookings = await bookingRepository.GetUpcomingBookingsByRoomAsync(roomId);
    return bookings;
  }
}