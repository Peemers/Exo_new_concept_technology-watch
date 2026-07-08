namespace BusinessRoomBooking.Core.Dtos.Room.Summaries;

public record RoomSummaryDto
{
  public required Guid Id { get; init; }
  public required string Location { get; init; }
}