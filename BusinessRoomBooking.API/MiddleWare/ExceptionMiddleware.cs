using System.Net;
using BusinessRoomBooking.Core.Exceptions;

namespace BusinessRoomBooking.MiddleWare;

public class ExceptionMiddleware(RequestDelegate next)
{
  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await next(context);
    }
    catch (Exception ex)
    {
      await HandleExceptionAsync(context, ex);
    }
  }
  private static Task HandleExceptionAsync(HttpContext context, Exception ex)
  {
    (int statusCode, string message) = ex switch
    {
      RoomNotFoundException => ((int)HttpStatusCode.NotFound, ex.Message),
      WorkerNotFoundException => ((int)HttpStatusCode.NotFound, ex.Message),
      BookingNotFoundException => ((int)HttpStatusCode.NotFound, ex.Message),
      BookingOverlapException => ((int)HttpStatusCode.Conflict, ex.Message),
      _ => ((int)HttpStatusCode.InternalServerError, "Une erreur interne est survenue.")
    };
    context.Response.StatusCode = statusCode;
    context.Response.ContentType = "application/json";
    return context.Response.WriteAsync(message);
  }
}