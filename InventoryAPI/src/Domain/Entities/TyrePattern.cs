using InventoryAPI.Domain.Enums;

namespace InventoryAPI.Domain.Entities;

public class TyrePattern : AuditableEntity
{
    public TyreBrand Brand { get; set; }

    public string Series { get; set; } = null!;
}