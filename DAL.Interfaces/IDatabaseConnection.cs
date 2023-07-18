using Microsoft.EntityFrameworkCore;

namespace DAL.Interfaces;

public interface IDatabaseConnection
{
    DbContext CreateDbContext();
    
    string ConnectionString { get; set; }
}