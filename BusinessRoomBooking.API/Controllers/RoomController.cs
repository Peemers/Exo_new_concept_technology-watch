using BusinessRoomBooking.Core.Dtos.Booking.Projections;
using BusinessRoomBooking.Core.Dtos.Room;
using BusinessRoomBooking.Core.Dtos.Room.Queries;
using BusinessRoomBooking.Core.Dtos.Room.Request;
using BusinessRoomBooking.Core.Dtos.Room.Response;
using BusinessRoomBooking.Core.Dtos.Room.Summaries;
using BusinessRoomBooking.Core.Dtos.RoomEquipment.Request;
using BusinessRoomBooking.Core.Exceptions.RoomExceptions;
using BusinessRoomBooking.Core.Interfaces.Repositories;
using BusinessRoomBooking.Core.Interfaces.Services;
using BusinessRoomBooking.Core.Mappers;
using BusinessRoomBooking.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BusinessRoomBooking.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomController(
  IRoomService roomService,
  IBaseRepository<Room> roomRepository) : ControllerBase
{
  [HttpGet]
  public async Task<ActionResult<IEnumerable<RoomSummaryDto>>> GetAvailableRoomsAsync([FromQuery] AvailableRoomsQueryDto queryDto)
  {
    IEnumerable<RoomSummaryDto> rooms = await roomService.GetAvailableRoomsAsync(queryDto);
    return Ok(rooms);
  }

  [HttpGet ("{roomId:guid}/bookings/upcoming")]
  public async Task<ActionResult<IEnumerable<UpcomingBookingDto>>> GetUpcomingBookingsByRoomAsync(Guid roomId)
  {
    IEnumerable<UpcomingBookingDto> bookings = await roomService.GetUpcomingBookingsByRoomAsync(roomId);
    return Ok(bookings);
  }
  
  [HttpPost("{roomId:guid}/equipments")]
  public async Task<IActionResult> AssignEquipmentToRoom(Guid roomId, [FromBody] AssignEquipmentToRoomRequestDto requestDto)
  {
    await roomService.AssignEquipmentToRoomAsync(roomId, requestDto);
    return NoContent();
  }
  [HttpGet("{id:guid}")]
  public async Task<ActionResult<RoomResponseDto>> GetRoomById(Guid id)
  {
    Room? room = await roomRepository.GetByIdAsync(id);
    if (room is null) throw new RoomNotFoundException(id);
    return Ok(room.ToRoomResponseDto());
  }

  [HttpPost]
  public async Task<ActionResult<RoomResponseDto>> CreateRoomAsync(CreateRoomRequestDto requestDto)
  {
    Room room = requestDto.ToRoom();
    
    await roomRepository.AddAsync(room);
    await roomRepository.SaveChangesAsync();
    
    RoomResponseDto roomResponseDto = room.ToRoomResponseDto();
    
    return CreatedAtAction(nameof(GetRoomById), new { id = room.Id }, roomResponseDto);
  }
}