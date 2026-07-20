using System.ComponentModel.DataAnnotations;

namespace BusinessRoomBooking.Core.Dtos.Worker.Request;

public record CreateWorkerRequestDto
{
  [Required]
  [StringLength(50, MinimumLength = 2, ErrorMessage = "Le prénom doit comporter entre 2 et 50 caracteres ")]
  public required string FirstName { get; init; }
  
  [Required]
  [StringLength(50, MinimumLength = 2, ErrorMessage = "Le nom de famille doit comporter entre 2 et 50 caracteres ")]
  public required string LastName { get; init; }
  
  [Required]
  [RegularExpression(@"^[\w\-\.]+@([\w-]+\.)+[\w-]{2,}$", ErrorMessage = "Email invalide")]
  public required string Email { get; init; }
}