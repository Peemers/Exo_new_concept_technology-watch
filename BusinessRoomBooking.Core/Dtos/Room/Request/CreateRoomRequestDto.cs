using System.ComponentModel.DataAnnotations;

namespace BusinessRoomBooking.Core.Dtos.Room.Request;

public record CreateRoomRequestDto
{
  [Required]
  [Range(1, 100, ErrorMessage = "Capacité entre 1 et 100 personnes.")]
  public required int MaxCapacity { get; init; }
  
  [Required]
  [StringLength(100, MinimumLength = 2, ErrorMessage = "La localisation de la salle doit comporter entre 2 et 100 caractères.")]
  public required string Location { get; init; }
  
  [Required]
  [StringLength(100, MinimumLength = 3, ErrorMessage = "Le nom de la salle doit comporter 3 caracteres minimum")]
  public required string Name { get; init; }
}