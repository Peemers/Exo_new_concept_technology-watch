using BusinessRoomBooking.Core.Dtos.Booking.Response;
using BusinessRoomBooking.Core.Exceptions.BookingExceptions;
using BusinessRoomBooking.Core.Interfaces.Repositories;
using BusinessRoomBooking.Core.Services;
using BusinessRoomBooking.Domain;
using NSubstitute;

namespace BusinessRoomBooking.Tests.Services;
public class BookingServiceTests
{
  [Fact]
  public async Task GetBookingById_ShouldReturnBooking()
  {
    //Arrange
    
    IBookingRepository bookingRepository = Substitute.For<IBookingRepository>();
    IBaseRepository<Room>  roomRepository = Substitute.For<IBaseRepository<Room>>();
    IBaseRepository<Worker>  workerRepository = Substitute.For<IBaseRepository<Worker>>();
    
    Guid roomId = Guid.NewGuid();
    Guid workerId = Guid.NewGuid();
    
    Room room = new Room
    {
      Id = roomId,
      Location = "Salle A",
      MaxCapacity = 10,
      Bookings = new List<Booking>(),
      RoomEquipments = new List<RoomEquipment>()
    };

    Worker worker = new Worker
    {
      Id = workerId,
      FirstName = "Mathieu",
      LastName = "Peemeros",
      Email = "Mathieu@SalleA.be",
      Bookings = new List<Booking>()
    };
    
    Booking booking = new Booking
    {
      Id = Guid.NewGuid(),
      StartDate = DateTime.UtcNow.AddDays(1),
      EndDate = DateTime.UtcNow.AddDays(1),
      NumberOfParticipant = 10,
      RoomId = roomId,
      WorkerId = workerId,
      Room =  room,
      Worker =  worker
    };
    
    BookingService bookingService = new BookingService(roomRepository, workerRepository, bookingRepository);
    
    bookingRepository.GetByIdAsync(booking.Id).Returns(booking);
    
    BookingResponseDto result = await bookingService.GetBookingByIdAsync(booking.Id);
    
    Assert.Equal(booking.Id, result.Id);
    Assert.Equal(room.Location, result.Room.Location);
    Assert.Equal(worker.FirstName, result.Worker.FirstName);
  }
  
  [Fact]
  public async Task GetBookingsById_ShouldThrow_WhenBookingNotFound()
  {
    IBookingRepository bookingRepository = Substitute.For<IBookingRepository>();
    IBaseRepository<Room>  roomRepository = Substitute.For<IBaseRepository<Room>>();
    IBaseRepository<Worker>  workerRepository = Substitute.For<IBaseRepository<Worker>>();
    
    Guid roomId = Guid.NewGuid();
    Guid workerId = Guid.NewGuid();

    Booking booking = new Booking
    {
      Id = Guid.NewGuid(),
      StartDate = DateTime.UtcNow.AddDays(1),
      EndDate = DateTime.UtcNow.AddDays(1),
      NumberOfParticipant = 10,
      RoomId = roomId,
      WorkerId = workerId,
    };
    
    bookingRepository.GetByIdAsync(booking.Id).Returns((Booking?)null);
    
    BookingService bookingService = new BookingService(roomRepository, workerRepository, bookingRepository);
    
    await Assert.ThrowsAsync<BookingNotFoundException>(() => bookingService.GetBookingByIdAsync(booking.Id));
  }
  
  [Fact]
  public async Task CancelBookingAsync_ShouldDeleteBooking()
  {
    IRoomRepository roomRepository = Substitute.For<IRoomRepository>();
    IBookingRepository bookingRepository = Substitute.For<IBookingRepository>();
    IBaseRepository<Worker>  workerRepository = Substitute.For<IBaseRepository<Worker>>();
    
    Guid roomId = Guid.NewGuid();
    Guid workerId = Guid.NewGuid();

    Room room = new Room
    {
      Id = roomId,
      Location = "Salle A",
      MaxCapacity = 10,
      Bookings = new List<Booking>(),
      RoomEquipments = new List<RoomEquipment>()
    };

    Worker worker = new Worker
    {
      Id = workerId,
      Email = "test@test.be",
      FirstName = "Test",
      LastName = "Test",
      Bookings = new List<Booking>()
    };


    Booking booking = new Booking
    {
      Id = Guid.NewGuid(),
      StartDate = DateTime.UtcNow.AddDays(1),
      EndDate = DateTime.UtcNow.AddDays(1),
      NumberOfParticipant = 18,
      RoomId = roomId,
      WorkerId = worker.Id,
      Room = room,
      Worker = worker
    };
    
    bookingRepository.GetByIdAsync(booking.Id).Returns(booking);
    
    BookingService bookingService = new BookingService(roomRepository, workerRepository, bookingRepository);
    
    await bookingService.CancelBookingAsync(booking.Id);
    
    await bookingRepository.Received(1).DeleteAsync(booking.Id);
  }

  [Fact]
  public async Task CancelBookingAsync_ShouldThrow_WhenBookingNotFound()
  {
    IBookingRepository bookingRepository = Substitute.For<IBookingRepository>();
    IBaseRepository<Room>  roomRepository = Substitute.For<IBaseRepository<Room>>();
    IBaseRepository<Worker>  workerRepository = Substitute.For<IBaseRepository<Worker>>();
    
    Guid roomId = Guid.NewGuid();
    Guid workerId = Guid.NewGuid();
    
    Room room = new Room
    {
      Id = roomId,
      Location = "Salle A",
      MaxCapacity = 10,
      Bookings = new List<Booking>(),
      RoomEquipments = new List<RoomEquipment>()
    };

    Worker worker = new Worker
    {
      Id = workerId,
      Email = "test@test.be",
      FirstName = "Test",
      LastName = "Test",
      Bookings = new List<Booking>()
    };


    Booking booking = new Booking
    {
      Id = Guid.NewGuid(),
      StartDate = DateTime.UtcNow.AddDays(1),
      EndDate = DateTime.UtcNow.AddDays(1),
      NumberOfParticipant = 18,
      RoomId = roomId,
      WorkerId = worker.Id,
      Room = room,
      Worker = worker
    };
    
    bookingRepository.GetByIdAsync(booking.Id).Returns((Booking?)null);
    
    BookingService bookingService = new BookingService(roomRepository, workerRepository, bookingRepository);
    
    await Assert.ThrowsAsync<BookingNotFoundException>(() => bookingService.CancelBookingAsync(booking.Id));
  }

  [Fact]

  public async Task CancelBookingAsync_ShouldThrow_WhenBookingIsInThePast()
  {
    IBookingRepository bookingRepository = Substitute.For<IBookingRepository>();
    IBaseRepository<Room>  roomRepository = Substitute.For<IBaseRepository<Room>>();
    IBaseRepository<Worker>  workerRepository = Substitute.For<IBaseRepository<Worker>>();
    
    Guid roomId = Guid.NewGuid();
    Guid workerId = Guid.NewGuid();
    
    Room room = new Room
    {
      Id = roomId,
      Location = "Salle A",
      MaxCapacity = 10,
      Bookings = new List<Booking>(),
      RoomEquipments = new List<RoomEquipment>()
    };

    Worker worker = new Worker
    {
      Id = workerId,
      Email = "test@test.be",
      FirstName = "Test",
      LastName = "Test",
      Bookings = new List<Booking>()
    };


    Booking booking = new Booking
    {
      Id = Guid.NewGuid(),
      StartDate = DateTime.UtcNow.AddDays(-1),
      EndDate = DateTime.UtcNow.AddDays(-1).AddHours(1),
      NumberOfParticipant = 18,
      RoomId = roomId,
      WorkerId = worker.Id,
      Room = room,
      Worker = worker
    };
    
    bookingRepository.GetByIdAsync(booking.Id).Returns(booking);
    
    BookingService bookingService = new BookingService(roomRepository, workerRepository, bookingRepository);
    
    await Assert.ThrowsAsync<BookingDateAlreadyPassedException>(() => bookingService.CancelBookingAsync(booking.Id));
  }
}