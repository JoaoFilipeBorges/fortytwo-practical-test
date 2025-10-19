using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Fortytwo.PracticalTest.Infrastructure.Persistence;

public class PracticalTestDbContextFactory : IDesignTimeDbContextFactory<PracticalTestDbContext>
{
    public PracticalTestDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PracticalTestDbContext>();

        var dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "practicaltest.db"
        );

        optionsBuilder.UseSqlite($"Data Source={dbPath}");

        return new PracticalTestDbContext(optionsBuilder.Options);
    }
}