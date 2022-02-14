namespace InventoryAPI.Domain.Entities;

public class TyreSize : AuditableEntity
{
    public int Width { get; set; }
    
    public int? Profile { get; set; }
    
    public int Diameter { get; set; }
}