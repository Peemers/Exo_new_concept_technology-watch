using BusinessRoomBooking.Core.Dtos.Room.Queries;
using BusinessRoomBooking.Core.Dtos.Room.Summaries;
using BusinessRoomBooking.Core.Dtos.RoomEquipment.Request;
using BusinessRoomBooking.Core.Exceptions.EquipmentExceptions;
using BusinessRoomBooking.Core.Exceptions.RoomExceptions;
using BusinessRoomBooking.Core.Interfaces.Repositories;
using BusinessRoomBooking.Core.Services;
using BusinessRoomBooking.Domain;
using NSubstitute;

namespace BusinessRoomBooking.Tests.Services;

public class RoomServiceTests
{
  [Fact]
  public async Task GetAvailableRoomsAsync_ShouldReturnRooms()
  {
    //Arrange

    IRoomRepository roomRepository = Substitute.For<IRoomRepository>();
    IRoomEquipmentRepository roomEquipmentRepository = Substitute.For<IRoomEquipmentRepository>();
    IBaseRepository<Equipment> equipmentRepository = Substitute.For<IBaseRepository<Equipment>>();
    IBookingRepository bookingRepository = Substitute.For<IBookingRepository>();

    List<RoomSummaryDto> roomSummary = new List<RoomSummaryDto>
    {
      new RoomSummaryDto { Id = Guid.NewGuid(), Location = "Salle A" }
    };

    roomRepository.GetAvailableRoomsAsync(Arg.Any<AvailableRoomsQueryDto>()).Returns(roomSummary);

    RoomService roomService = new RoomService(roomRepository, roomEquipmentRepository, equipmentRepository, bookingRepository);
    AvailableRoomsQueryDto queryDto = new AvailableRoomsQueryDto
    {
      StartDate = new DateTime(2026, 8, 1, 14, 0, 0),
      EndDate = new DateTime(2026, 8, 1, 16, 0, 0),
      MinCapacity = 5
    };

    //Act

    IEnumerable<RoomSummaryDto> result = await roomService.GetAvailableRoomsAsync(queryDto);

    //Assert

    Assert.Equal(roomSummary, result);
  }

  [Fact]
  public async Task AssignRoomAsync_ShouldAssignEquipmentToRoom()
  {
    //Arrange
    IRoomRepository roomRepository = Substitute.For<IRoomRepository>();
    IRoomEquipmentRepository roomEquipmentRepository = Substitute.For<IRoomEquipmentRepository>();
    IBaseRepository<Equipment> equipmentRepository = Substitute.For<IBaseRepository<Equipment>>();
    IBookingRepository bookingRepository = Substitute.For<IBookingRepository>();

    Guid roomId = Guid.NewGuid();
    Guid equipmentId = Guid.NewGuid();

    Equipment equipment = new Equipment
    {
      Id = equipmentId,
      Name = "Test"
    };

    AssignEquipmentToRoomRequestDto assignDto = new AssignEquipmentToRoomRequestDto
    {
      EquipmentId = equipmentId,
    };

    Room room = new Room
    {
      Id = roomId,
      Location = "Salle A",
      MaxCapacity = 10,
      RoomEquipments = new List<RoomEquipment>()
    };
    RoomService roomService = new RoomService(roomRepository, roomEquipmentRepository, equipmentRepository, bookingRepository);
    roomEquipmentRepository.EquipmentAlreadyExistInRoomAsync(roomId, equipmentId).Returns(false);

    roomRepository.GetByIdAsync(roomId).Returns(room);
    equipmentRepository.GetByIdAsync(equipmentId).Returns(equipment);

    //Act

    await roomService.AssignEquipmentToRoomAsync(roomId, assignDto);

    //Assert

    await roomEquipmentRepository.Received(1).AddAsync(Arg.Any<RoomEquipment>());
  }

  [Fact]
  public async Task AssignEquipmentToRoomRequestDto_ShouldThrow_WhenRoomNotFound()
  {
    //Arrange

    IRoomRepository roomRepository = Substitute.For<IRoomRepository>();
    IRoomEquipmentRepository roomEquipmentRepository = Substitute.For<IRoomEquipmentRepository>();
    IBaseRepository<Equipment> equipmentRepository = Substitute.For<IBaseRepository<Equipment>>();
    IBookingRepository bookingRepository = Substitute.For<IBookingRepository>();

    Guid roomId = Guid.NewGuid();
    Guid equipmentId = Guid.NewGuid();

    AssignEquipmentToRoomRequestDto assignDto = new AssignEquipmentToRoomRequestDto
    {
      EquipmentId = equipmentId,
    };

    roomRepository.GetByIdAsync(roomId).Returns((Room?)null);

    RoomService roomService = new RoomService(roomRepository, roomEquipmentRepository, equipmentRepository, bookingRepository);

    //Act

    await Assert.ThrowsAsync<RoomNotFoundException>(() => roomService.AssignEquipmentToRoomAsync(roomId, assignDto));
  }

  [Fact]
  public async Task AssignEquipmentToRoomRequestDto_ShouldThrow_WhenEquipmentNotFound()
  {
    IRoomRepository roomRepository = Substitute.For<IRoomRepository>();
    IRoomEquipmentRepository roomEquipmentRepository = Substitute.For<IRoomEquipmentRepository>();
    IBaseRepository<Equipment> equipmentRepository = Substitute.For<IBaseRepository<Equipment>>();
    IBookingRepository bookingRepository = Substitute.For<IBookingRepository>();

    Guid roomId = Guid.NewGuid();
    Guid equipmentId = Guid.NewGuid();

    Room room = new Room
    {
      Id = roomId,
      Location = "Salle A",
      MaxCapacity = 10,
    };
    
    AssignEquipmentToRoomRequestDto assignDto = new AssignEquipmentToRoomRequestDto
    {
      EquipmentId = equipmentId,
    };
    
    roomRepository.GetByIdAsync(roomId).Returns(room);
    equipmentRepository.GetByIdAsync(equipmentId).Returns((Equipment?)null);

    RoomService roomService = new RoomService(roomRepository, roomEquipmentRepository, equipmentRepository, bookingRepository);

    await Assert.ThrowsAsync<EquipmentNotFoundException>(() => roomService.AssignEquipmentToRoomAsync(roomId, assignDto));
  }

  [Fact]
  public async Task AssignEquipmentToRoomRequestDto_ShouldThrow_WhenEquipmentAlreadyExistsInRoom()
  {
    IRoomRepository roomRepository = Substitute.For<IRoomRepository>();
    IRoomEquipmentRepository roomEquipmentRepository = Substitute.For<IRoomEquipmentRepository>();
    IBaseRepository<Equipment> equipmentRepository = Substitute.For<IBaseRepository<Equipment>>();
    IBookingRepository bookingRepository = Substitute.For<IBookingRepository>();

    Guid roomId = Guid.NewGuid();
    Guid equipmentId = Guid.NewGuid();

    AssignEquipmentToRoomRequestDto assignDto = new AssignEquipmentToRoomRequestDto
    {
      EquipmentId = equipmentId,
    };
    
    Room room = new Room{Id = roomId, Location = "Salle A", MaxCapacity = 10};
    Equipment equipment = new Equipment{Id = equipmentId, Name = "Test"};
    
    roomRepository.GetByIdAsync(roomId).Returns(room);
    equipmentRepository.GetByIdAsync(equipmentId).Returns(equipment);
    roomEquipmentRepository.EquipmentAlreadyExistInRoomAsync(roomId, equipmentId).Returns(true);

    RoomService roomService = new RoomService(roomRepository, roomEquipmentRepository, equipmentRepository, bookingRepository);

    await Assert.ThrowsAsync<EquipmentAlreadyAssignedException>(() => roomService.AssignEquipmentToRoomAsync(roomId, assignDto));
  }
}