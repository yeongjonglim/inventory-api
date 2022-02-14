using InventoryAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryAPI.Infrastructure.Persistence.Configurations;

public class TyrePatternConfiguration : IEntityTypeConfiguration<TyrePattern>
{
    public void Configure(EntityTypeBuilder<TyrePattern> builder)
    {
        builder.Property(t => t.Brand)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(t => t.Series)
            .HasMaxLength(200)
            .IsRequired();
    }
}