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
      Location = "1er étage",
      MaxCapacity = 10,
      Name = "Salle A"
    };
    
    //Act
    
    RoomResponseDto dto = room.ToRoomResponseDto();
    
    //Assert
    
    Assert.Equal(room.Id, dto.Id);
    Assert.Equal(room.Location, dto.Location);
    Assert.Equal(room.MaxCapacity, dto.MaxCapacity);
    Assert.Equal(room.Name, dto.Name);
  }

  [Fact]
  public void ToRoom_ShouldMapCorrectly()
  {
    //Arrange

    CreateRoomRequestDto dto = new CreateRoomRequestDto
    {
      Location = "1er étage",
      MaxCapacity = 10,
      Name =  "Salle A"
    };
    
    //Act
    
    Room room = dto.ToRoom();
    
    //Assert
    
    Assert.Equal(dto.Location, room.Location);
    Assert.Equal(dto.MaxCapacity, room.MaxCapacity);
    Assert.Equal(dto.Name, room.Name);
  }
}