using BusinessRoomBooking.Core.Dtos.Booking.Request;
using BusinessRoomBooking.Core.Dtos.Booking.Response;
using BusinessRoomBooking.Core.Exceptions;
using BusinessRoomBooking.Core.Interfaces.Repositories;
using BusinessRoomBooking.Core.Interfaces.Services;
using BusinessRoomBooking.Core.Mappers;
using BusinessRoomBooking.Domain;

namespace BusinessRoomBooking.Core.Services;

public class BookingService(
  IBaseRepository<Room> roomRepository,
  IBaseRepository<Worker> workerRepository,
  IBookingRepository bookingRepository) : IBookingService
{
  public async Task<BookingResponseDto> CreateBookingAsync(CreateBookingRequestDto dto)
  {
    Room? room = await roomRepository.GetByIdAsync(dto.RoomId);
    if (room is null)
    {
      throw new RoomNotFoundException(dto.RoomId);
    }

    Worker? worker = await workerRepository.GetByIdAsync(dto.WorkerId);

    if (worker is null)
    {
      throw new WorkerNotFoundException(dto.WorkerId);
    }
    
    bool hasOverlap = await bookingRepository.HasOverlapAsync(dto.RoomId, dto.StartDate, dto.EndDate);
    if (hasOverlap)
    {
      throw new BookingOverlapException(dto.RoomId, dto.StartDate, dto.EndDate);
    }

    Booking booking = dto.ToBooking(room, worker);
    
    await bookingRepository.AddAsync(booking);
    await bookingRepository.SaveChangesAsync();

    return booking.ToBookingResponseDto();
  }

  public async Task<BookingResponseDto> GetBookingByIdAsync(Guid id)
  {
    Booking? booking = await bookingRepository.GetByIdAsync(id);
    if (booking is null)
    {
      throw new BookingNotFoundException(id);
    }
    return booking.ToBookingResponseDto();
  }
}