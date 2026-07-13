namespace BusinessRoomBooking.Core.Exceptions.EquipmentExceptions;

public class EquipmentNotFoundException : Exception
{
  public EquipmentNotFoundException(Guid equipmentId)
    : base($"L'équipement {equipmentId} n'existe pas.")
  {
    
  }
}