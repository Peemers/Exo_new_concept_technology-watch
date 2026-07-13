namespace BusinessRoomBooking.Core.Exceptions.BookingExceptions;

public class BookingOverlapException : Exception
{
  public BookingOverlapException(Guid roomId, DateTime startDate, DateTime endDate)
    : base($"La salle {roomId} a déjà une réservation qui chevauche la période du {startDate} au {endDate}.")
  {
  }
}