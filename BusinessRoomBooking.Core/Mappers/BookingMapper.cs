using BusinessRoomBooking.Core.Dtos.Booking.Request;
using BusinessRoomBooking.Core.Dtos.Booking.Response;
using BusinessRoomBooking.Core.Dtos.Room.Summaries;
using BusinessRoomBooking.Core.Dtos.Worker.Summaries;
using BusinessRoomBooking.Domain;

namespace BusinessRoomBooking.Core.Mappers;

public static class BookingMapper
{
  public static BookingResponseDto ToBookingResponseDto(this Booking booking)
  {
    RoomSummaryDto roomSummary = new RoomSummaryDto
    {
      Id = booking.Room.Id,
      Location = booking.Room.Location
    };

    WorkerSummaryDto workerSummary = new WorkerSummaryDto
    {
      Id = booking.WorkerId,
      FirstName = booking.Worker.FirstName,
      LastName = booking.Worker.LastName,
    };

    return new BookingResponseDto
    {
      Id = booking.Id,
      StartDate = booking.StartDate,
      EndDate = booking.EndDate,
      NumberOfParticipant = booking.NumberOfParticipant,
      Room = roomSummary,
      Worker = workerSummary
    };
  }

  public static Booking ToBooking(this CreateBookingRequestDto dto, Room room, Worker worker)
  {
    return new Booking
    {
      Id = Guid.NewGuid(),
      StartDate = dto.StartDate,
      EndDate = dto.EndDate,
      NumberOfParticipant = dto.NumberOfParticipant,
      Room = room,
      Worker = worker
    };
  }

  public static void UpdateFromDto(this Booking booking, UpdateBookingRequestDto dto)
  {
    booking.StartDate = dto.StartDate;
    booking.EndDate = dto.EndDate;
    booking.NumberOfParticipant = dto.NumberOfParticipant;
  }
}