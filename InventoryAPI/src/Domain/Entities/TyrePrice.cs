namespace InventoryAPI.Domain.Entities;

public class TyrePrice : AuditableEntity
{
    public TyreSize TyreSize { get; set; } = null!;

    public TyrePattern TyrePattern { get; set; } = null!;
    
    public decimal? SellingPrice { get; set; }
}