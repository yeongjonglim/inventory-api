namespace InventoryAPI.Application.Extensions;

public static class EnvironmentExtensions
{
    public static string GetEnvironmentVariable(string name, string defaultValue)
        => Environment.GetEnvironmentVariable(name) is {Length: > 0} v ? v : defaultValue;
}