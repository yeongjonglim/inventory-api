using InventoryAPI.Application.Common.Interfaces;

namespace InventoryAPI.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}