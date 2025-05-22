using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PVDNOTE.Backend.Infrastructure.Data;

public class DBContextFactory : IDesignTimeDbContextFactory<DBContext>
{
    public DBContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DBContext>();
        optionsBuilder.UseNpgsql("Host=localhost;port=5432;Database=PVD_Note;Username=postgres;Password=1111;");
        
        return new DBContext(optionsBuilder.Options);
    }
}