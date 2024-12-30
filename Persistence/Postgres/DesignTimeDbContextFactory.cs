using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Persistence.Postgres
{

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=webauto_db;Username=gnunez;Password=test123!");

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
