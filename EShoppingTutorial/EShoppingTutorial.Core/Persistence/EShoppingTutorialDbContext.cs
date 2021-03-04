using EShoppingTutorial.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EShoppingTutorial.Core.Persistence
{
    public class EShoppingTutorialDbContext : DbContext
    {
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }

        public EShoppingTutorialDbContext(DbContextOptions<EShoppingTutorialDbContext> dbContextOptions) 
            : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Scans a given assembly for all types that implement IEntityTypeConfiguration, and registers each one automatically
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
