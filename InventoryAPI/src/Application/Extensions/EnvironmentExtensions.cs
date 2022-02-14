namespace InventoryAPI.Application.Extensions;

public static class EnvironmentExtensions
{
    public static string GetEnvironmentVariable(string name, string defaultValue)
        => Environment.GetEnvironmentVariable(name) is {Length: > 0} v ? v : defaultValue;

    public static string GetDatabaseUrl(string name, string defaultConnectionString)
    {
        var connectionString = GetEnvironmentVariable(name, defaultConnectionString);
        
        // If connection string use postgres default style
        if (connectionString.StartsWith("postgres://"))
        {
            var uri = new Uri(connectionString);

            var username = uri.UserInfo.Split(':')[0];

            var password = uri.UserInfo.Split(':')[1];

            var host = uri.Host;

            connectionString = "Host=" + host + 
                               "; Database=" + uri.AbsolutePath[1..] +
                               "; Username=" + username +
                               "; Password=" + password +
                               "; Port=" + uri.Port +
                               "; SSL Mode=Require; Trust Server Certificate=true;";
        }

        return connectionString;
    }
}