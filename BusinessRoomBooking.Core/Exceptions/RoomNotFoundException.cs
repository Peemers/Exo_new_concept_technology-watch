namespace BusinessRoomBooking.Core.Exceptions;

public class RoomNotFoundException : Exception
{
  public RoomNotFoundException(Guid roomId)
    : base($"La room {roomId} que vous cherchez n'existe pas.")
  {
    
  }
}