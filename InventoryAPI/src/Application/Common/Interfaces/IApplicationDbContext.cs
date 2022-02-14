using InventoryAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryAPI.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TyrePattern> TyrePatterns { get; }
    DbSet<TyrePrice> TyrePrices { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}