namespace BusinessRoomBooking.Core.Exceptions;

public class WorkerNotFoundException : Exception
{
  public WorkerNotFoundException (Guid workerId)
    : base($"Le worker {workerId} est introuvable")
  {
  }
}