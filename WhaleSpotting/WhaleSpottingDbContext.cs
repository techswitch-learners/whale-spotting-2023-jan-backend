using Microsoft.EntityFrameworkCore;
using Npgsql;
using WhaleSpotting.Models.Database;

namespace WhaleSpotting;

public class WhaleSpottingDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<WhaleSighting> WhaleSightings { get; set; }
    public DbSet<WhaleSpecies> WhaleSpecies { get; set; }
    public DbSet<Like> Likes { get; set; }

    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder
    )
    {
        optionsBuilder.UseNpgsql(GetConnectionString());
    }

    private static string GetConnectionString()
    {
        var databaseUrl =
            Environment.GetEnvironmentVariable("DATABASE_URL");
        if (databaseUrl == null)
        {
            throw new Exception("Environment variable 'DATABASE_URL' must not be null");
        }

        bool useSsl = true;
        var useSslVariable = Environment.GetEnvironmentVariable("USE_SSL");
        if (useSslVariable != null)
        {
            if (!Boolean.TryParse(useSslVariable, out useSsl))
            {
                throw new Exception("Environment variable 'USE_SSL' must be parse-able as bool");
            }
        }

        var databaseUri = new Uri(databaseUrl);
        var userInfo = databaseUri.UserInfo.Split(':');

        var builder =
            new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/')
            };
        if (!useSsl) return builder.ToString();
        builder.SslMode = SslMode.Require;
        builder.TrustServerCertificate = true;

        return builder.ToString();
    }
}