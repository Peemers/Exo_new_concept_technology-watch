using BusinessRoomBooking.Core.Dtos.Room.Request;
using BusinessRoomBooking.Core.Dtos.Room.Response;
using BusinessRoomBooking.Core.Dtos.Worker.Response;
using BusinessRoomBooking.Core.Mappers;
using BusinessRoomBooking.Domain;

namespace BusinessRoomBooking.Tests.Mappers;

public class RoomMapperTests
{
  [Fact]
  public void ToRoomResponseDto_ShouldMapCorrectly()
  {
    //Arrange

    Room room = new Room
    {
      Id = Guid.NewGuid(),
      Location = "Salle A",
      MaxCapacity = 10,
    };
    
    //Act
    
    RoomResponseDto dto = room.ToRoomResponseDto();
    
    //Assert
    
    Assert.Equal(room.Id, dto.Id);
    Assert.Equal(room.Location, dto.Location);
    Assert.Equal(room.MaxCapacity, dto.MaxCapacity);
  }

  [Fact]
  public void ToRoom_ShouldMapCorrectly()
  {
    //Arrange

    CreateRoomRequestDto dto = new CreateRoomRequestDto
    {
      Location = "Salle A",
      MaxCapacity = 10,
    };
    
    //Act
    
    Room room = dto.ToRoom();
    
    //Assert
    
    Assert.Equal(dto.Location, room.Location);
    Assert.Equal(dto.MaxCapacity, room.MaxCapacity);
  }
}