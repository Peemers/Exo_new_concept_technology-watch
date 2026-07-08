using BusinessRoomBooking.Core.Dtos.Room.Summaries;
using BusinessRoomBooking.Core.Dtos.Worker.Summaries;

namespace BusinessRoomBooking.Core.Dtos.Booking.Response;

public record BookingResponseDto
{
  public required Guid Id { get; init; }

  public required DateTime StartDate { get; init; }

  public required DateTime EndDate { get; init; }

  public required int NumberOfParticipant { get; init; }

  public required RoomSummaryDto Room { get; init; }
  
  public required WorkerSummaryDto Worker { get; init; }
}