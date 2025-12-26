using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EShoppingTutorial.Core.Persistence;

public class EShoppingTutorialDbContext(DbContextOptions<EShoppingTutorialDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public virtual DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Scans a given assembly for all types that implement IEntityTypeConfiguration, and registers each one automatically
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
