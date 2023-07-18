using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL.SQL;

public class PgDatabaseConnection : IDatabaseConnection
{
    private const string MigrationAssemblyName = "DAL.SQL";

    public PgDatabaseConnection(IConfiguration configuration)
    {
        ConnectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public DbContext CreateDbContext()
    {
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        dbContextOptionsBuilder.UseNpgsql(this.ConnectionString);
        return new AppDbContext(dbContextOptionsBuilder.Options);
    }

    public string ConnectionString { get; set; }
}