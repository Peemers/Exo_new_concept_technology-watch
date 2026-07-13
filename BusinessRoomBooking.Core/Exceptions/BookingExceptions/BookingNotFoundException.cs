namespace BusinessRoomBooking.Core.Exceptions.BookingExceptions;

public class BookingNotFoundException : Exception
{
  public BookingNotFoundException(Guid bookingId)
    : base($"La réservation {bookingId} n'existe pas")
  {
    
  }
}