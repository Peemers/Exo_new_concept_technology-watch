using BusinessRoomBooking.Core.Dtos.Booking.Request;
using BusinessRoomBooking.Core.Dtos.Booking.Response;

namespace BusinessRoomBooking.Core.Interfaces.Services;

public interface IBookingService
{
  Task<BookingResponseDto> CreateBookingAsync(CreateBookingRequestDto dto);
  Task<BookingResponseDto> GetBookingByIdAsync(Guid id);
}