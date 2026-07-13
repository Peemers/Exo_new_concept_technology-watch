using BusinessRoomBooking.Core.Dtos.Room.Queries;
using BusinessRoomBooking.Core.Dtos.Room.Summaries;
using BusinessRoomBooking.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusinessRoomBooking.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomController(
  IRoomService roomService) : ControllerBase
{
  [HttpGet]
  public async Task<ActionResult<IEnumerable<RoomSummaryDto>>> GetAvailableRoomsAsync([FromQuery] AvailableRoomsQueryDto queryDto)
  {
    IEnumerable<RoomSummaryDto> rooms = await roomService.GetAvailableRoomsAsync(queryDto);
    return Ok(rooms);
  }
}