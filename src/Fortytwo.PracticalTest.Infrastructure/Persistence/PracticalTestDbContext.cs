using Fortytwo.PracticalTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fortytwo.PracticalTest.Infrastructure.Persistence;

public class PracticalTestDbContext : DbContext
{
    public DbSet<Todo> Todos { get; set; }
    public DbSet<User> Users { get; set; }
    
    public PracticalTestDbContext(DbContextOptions<PracticalTestDbContext> options)
        : base(options)
    {
    }
}