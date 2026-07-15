using BusinessRoomBooking.Core.Dtos.RoomEquipment.Request;
using BusinessRoomBooking.Core.Mappers;
using BusinessRoomBooking.Domain;

namespace BusinessRoomBooking.Tests.Mappers;

public class RoomEquipmentTest
{
  [Fact]
  public void ToRoomEquipment_ShouldMapCorrectly()
  {
    //Arrange

    Guid roomId = Guid.NewGuid();
    Guid equipmentId = Guid.NewGuid();

    AssignEquipmentToRoomRequestDto assignDto = new AssignEquipmentToRoomRequestDto
    {
      EquipmentId = equipmentId,
    };

    //Act
    RoomEquipment roomEquipment = assignDto.ToRoomEquipment(roomId);

    //Assert

    Assert.Equal(roomId, roomEquipment.RoomId);
    Assert.Equal(equipmentId, roomEquipment.EquipmentId);
  }
}