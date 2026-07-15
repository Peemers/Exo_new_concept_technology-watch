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

    Room room = new Room { Id = Guid.NewGuid(), MaxCapacity = 10, Location = "Salle A" };
    Worker worker = new Worker{Id = Guid.NewGuid(), FirstName =  "John", LastName = "Doe", Email = "john.doe@mail.be"};
    Booking booking = new Booking
    {
      Id = Guid.NewGuid(),
      NumberOfParticipant = 5,
      Room = room,
      Worker = worker,
      StartDate = new DateTime(2026, 8, 1, 14, 0, 0),
      EndDate = new DateTime(2026, 8, 1, 14, 0, 0),
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
    
    Room room = new Room { Id = Guid.NewGuid(), MaxCapacity = 10, Location = "Salle A" };
    Worker worker = new Worker { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Email = "john.doe@mail.be" };
    CreateBookingRequestDto bookingRequestDto = new CreateBookingRequestDto
    {
      EndDate = new DateTime(2026, 8, 1, 16, 0, 0),
      StartDate = new DateTime(2026, 8, 1, 14, 0, 0),
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
      StartDate = new DateTime(2026, 8, 1, 14, 0, 0),
      EndDate = new DateTime(2026, 8, 1, 15, 0, 0),
    };
    UpdateBookingRequestDto requestDto = new UpdateBookingRequestDto
    {
      StartDate = new DateTime(2026, 8, 1, 15, 0, 0),
      EndDate = new DateTime(2026, 8, 1, 16, 0, 0),
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