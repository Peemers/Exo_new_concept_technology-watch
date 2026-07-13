namespace BusinessRoomBooking.Core.Exceptions.BookingExceptions;

public class BookingDateAlreadyPassedException : Exception
{
  public BookingDateAlreadyPassedException(DateTime startDate, Guid bookingId)
    : base($"Date {startDate} de la réservation {bookingId} est dépassée.")
  {
    
  }
}