namespace BusinessRoomBooking.Core.Exceptions;

public class BookingDateAlreadyPassedException : Exception
{
  public BookingDateAlreadyPassedException(DateTime startDate, Guid bookingId)
    : base($"Date {startDate} de la réservation {bookingId} est dépassée.")
  {
    
  }
}