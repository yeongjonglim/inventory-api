using InventoryAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryAPI.Infrastructure.Persistence.Configurations;

public class TyrePriceConfiguration : IEntityTypeConfiguration<TyrePrice>
{
    public void Configure(EntityTypeBuilder<TyrePrice> builder)
    {
        builder.Property(t => t.CreatedBy)
            .IsRequired();
        
        builder.Property(t => t.Created)
            .IsRequired();
        
        builder.Property(t => t.LastModified)
            .IsRequired();
        
        builder.Property(t => t.LastModifiedBy)
            .IsRequired();

        builder.HasOne(x => x.TyrePattern);
        builder.HasOne(x => x.TyreSize);
    }
}