namespace BusinessRoomBooking.Core.Dtos.Room.Response;

public record RoomResponseDto
{
  public Guid Id { get; init; }
  public required int MaxCapacity { get; init; }
  public required string Location  { get; init; }
}