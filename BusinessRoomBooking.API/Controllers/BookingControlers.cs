using BusinessRoomBooking.Core.Dtos.Booking.Request;
using BusinessRoomBooking.Core.Dtos.Booking.Response;
using BusinessRoomBooking.Core.Interfaces.Services;
using BusinessRoomBooking.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BusinessRoomBooking.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BookingController
  (IBookingService bookingService) : ControllerBase 
{
  [HttpPost]
  public async Task<ActionResult<BookingResponseDto>> CreateBookingAsync([FromBody] CreateBookingRequestDto dto)
  {
    BookingResponseDto booking = await bookingService.CreateBookingAsync(dto);
    return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
  }

  [HttpGet("{id:guid}")]
  public async Task<ActionResult<BookingResponseDto>> GetById(Guid id)
  {
    BookingResponseDto booking = await bookingService.GetBookingByIdAsync(id);
    return Ok(booking);
  }
}