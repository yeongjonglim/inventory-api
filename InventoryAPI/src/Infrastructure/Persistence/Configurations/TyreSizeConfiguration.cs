using InventoryAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryAPI.Infrastructure.Persistence.Configurations;

public class TyreSizeConfiguration : IEntityTypeConfiguration<TyreSize>
{
    public void Configure(EntityTypeBuilder<TyreSize> builder)
    {
        builder.Property(t => t.CreatedBy)
            .IsRequired();
        
        builder.Property(t => t.Created)
            .IsRequired();
        
        builder.Property(t => t.LastModified)
            .IsRequired();
        
        builder.Property(t => t.LastModifiedBy)
            .IsRequired();
    }
}