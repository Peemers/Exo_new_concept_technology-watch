using BusinessRoomBooking.Controllers;
using BusinessRoomBooking.Core.Dtos.Booking.Response;
using BusinessRoomBooking.Core.Dtos.Room.Request;
using BusinessRoomBooking.Core.Dtos.Room.Response;
using BusinessRoomBooking.Core.Interfaces.Repositories;
using BusinessRoomBooking.Core.Interfaces.Services;
using BusinessRoomBooking.Core.Mappers;
using BusinessRoomBooking.Domain;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace BusinessRoomBooking.Tests.Controllers;

public class RoomControllerTests
{
  [Fact]
  public async Task CreateRoomAsync_ShouldReturnCreatedRoom()
  {
    //Arrange
    
    IRoomService roomService = Substitute.For<IRoomService>();
    IBaseRepository<Room> roomRepository = Substitute.For<IBaseRepository<Room>>();

    CreateRoomRequestDto dto = new CreateRoomRequestDto
    {
      MaxCapacity = 100,
      Location = "Salle A",
    };
    
    RoomController controller = new RoomController(roomService, roomRepository);
    
    //Act
    
    ActionResult<RoomResponseDto> result = await controller.CreateRoomAsync(dto);
    
    //Assert
    
    CreatedAtActionResult createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
    RoomResponseDto roomResponseDto = Assert.IsType<RoomResponseDto>(createdResult.Value);
    
    Assert.Equal(dto.MaxCapacity, roomResponseDto.MaxCapacity);
    Assert.Equal(dto.Location, roomResponseDto.Location);
    
    await roomRepository.Received(1).AddAsync(Arg.Any<Room>());
    await roomRepository.Received(1).SaveChangesAsync();
  }

  [Fact]
  public async Task GetRoomById_ShouldReturnRoom()
  {
    //Arrange
    
    IRoomService roomService = Substitute.For<IRoomService>();
    IBaseRepository<Room> roomRepository = Substitute.For<IBaseRepository<Room>>();

    Room room = new Room
    {
      Location = "Salle A",
      MaxCapacity = 100,
      Id = Guid.NewGuid(),
    };
    
    roomRepository.GetByIdAsync(room.Id).Returns(room);
    
    RoomController controller = new RoomController(roomService, roomRepository);
    
    //Act
    ActionResult<RoomResponseDto> result = await controller.GetRoomById(room.Id);
    
    //Assert
    
    OkObjectResult okResult = Assert.IsType<OkObjectResult>(result.Result);
    RoomResponseDto roomResponseDto = Assert.IsType<RoomResponseDto>(okResult.Value);
    
    Assert.Equal(room.Id, roomResponseDto.Id);
    Assert.Equal(room.MaxCapacity, roomResponseDto.MaxCapacity);
    Assert.Equal(room.Location, roomResponseDto.Location);
  }
}