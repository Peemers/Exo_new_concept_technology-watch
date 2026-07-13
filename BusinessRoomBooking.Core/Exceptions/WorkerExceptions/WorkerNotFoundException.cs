namespace BusinessRoomBooking.Core.Exceptions.WorkerExceptions;

public class WorkerNotFoundException : Exception
{
  public WorkerNotFoundException (Guid workerId)
    : base($"Le worker {workerId} est introuvable")
  {
  }
}