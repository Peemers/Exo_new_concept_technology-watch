using BusinessRoomBooking.Core.Dtos.Booking.Request;
using BusinessRoomBooking.Core.Dtos.Booking.Response;
using BusinessRoomBooking.Core.Mappers;
using BusinessRoomBooking.Domain;

namespace BusinessRoomBooking.Tests.Mappers;

public class BookingMapperTests
{
  [Fact]
  public void ToBookingResponseDto_ShouldMapCorrectly()
  {
    //Arrange

    Room room = new Room { Id = Guid.NewGuid(), MaxCapacity = 10, Location = "1er étage", Name = "Salle A" };
    Worker worker = new Worker{Id = Guid.NewGuid(), FirstName =  "John", LastName = "Doe", Email = "john.doe@mail.be"};
    Booking booking = new Booking
    {
      Id = Guid.NewGuid(),
      NumberOfParticipant = 5,
      Room = room,
      Worker = worker,
      StartDate = DateTime.UtcNow.AddDays(1),
      EndDate = DateTime.UtcNow.AddDays(1),
    };
    //Act
    BookingResponseDto result = booking.ToBookingResponseDto();
    
    //Assert
    Assert.Equal(booking.Id, result.Id);
    Assert.Equal(room.Location, result.Room.Location);
    Assert.Equal(worker.FirstName, result.Worker.FirstName);
  }
  [Fact]
  public void ToBooking_ShouldMapCorrectly()
  {
    //Arrange
    
    Room room = new Room { Id = Guid.NewGuid(), MaxCapacity = 10, Location = "1er étage", Name = "Salle A"};
    Worker worker = new Worker { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Email = "john.doe@mail.be" };
    CreateBookingRequestDto bookingRequestDto = new CreateBookingRequestDto
    {
      EndDate = DateTime.UtcNow.AddDays(1) ,
      StartDate = DateTime.UtcNow.AddDays(1),
      NumberOfParticipant = 10,
      RoomId = room.Id,
      WorkerId = worker.Id,
    };
    
    //Act
    
    Booking booking = bookingRequestDto.ToBooking(room, worker);
    
    //Assert
    
    Assert.Equal(bookingRequestDto.StartDate, booking.StartDate);
    Assert.Equal(bookingRequestDto.EndDate, booking.EndDate);
    Assert.Equal(bookingRequestDto.NumberOfParticipant, booking.NumberOfParticipant);
    Assert.Equal(bookingRequestDto.RoomId, booking.RoomId);
    Assert.Equal(bookingRequestDto.WorkerId, booking.WorkerId);
    Assert.NotEqual(Guid.Empty, booking.Id);
  }
  [Fact]
  public void UpdateFromDto_ShouldMapCorrectly()
  {
    //Arrange

    Booking booking = new Booking
    {
      Id = Guid.NewGuid(),
      NumberOfParticipant = 10,
      RoomId = Guid.NewGuid(),
      StartDate = DateTime.UtcNow.AddDays(1),
      EndDate = DateTime.UtcNow.AddDays(1)
    };
    UpdateBookingRequestDto requestDto = new UpdateBookingRequestDto
    {
      StartDate = DateTime.UtcNow.AddDays(1),
      EndDate = DateTime.UtcNow.AddDays(1),
      NumberOfParticipant = 8,
    };
    
    //Act
    
    booking.UpdateFromDto(requestDto);
    
    //Assert
    
    Assert.Equal(requestDto.StartDate, booking.StartDate);
    Assert.Equal(requestDto.EndDate, booking.EndDate);
    Assert.Equal(requestDto.NumberOfParticipant, booking.NumberOfParticipant);
  }
}