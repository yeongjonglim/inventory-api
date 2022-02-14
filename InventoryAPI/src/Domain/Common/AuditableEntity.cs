namespace InventoryAPI.Domain.Common;

public abstract class AuditableEntity : IdentifiableEntity
{
    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime LastModified { get; set; }

    public string LastModifiedBy { get; set; } = null!;
}