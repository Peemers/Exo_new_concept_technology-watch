namespace BusinessRoomBooking.Core.Exceptions.EquipmentExceptions;

public class EquipmentAlreadyAssignedException : Exception
{
  public EquipmentAlreadyAssignedException(Guid equipmentId, Guid roomId)
    : base($"L'équipement {equipmentId} est déja présent dans la room {roomId}")
  {
    
  }
}