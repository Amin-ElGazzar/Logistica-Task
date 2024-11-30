using System.Reflection;
using LogisticaSolutions.Entities;
using Microsoft.EntityFrameworkCore;


namespace LogisticaSolutions.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products => Set<Product>();


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
