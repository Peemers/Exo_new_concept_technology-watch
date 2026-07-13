namespace BusinessRoomBooking.Core.Exceptions.RoomExceptions;

public class RoomNotFoundException : Exception
{
  public RoomNotFoundException(Guid roomId)
    : base($"La room {roomId} que vous cherchez n'existe pas.")
  {
    
  }
}