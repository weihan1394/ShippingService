using ShippingService.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ShippingService.Core
{
    public partial class CarsContext : DbContext
    {
        public CarsContext(DbContextOptions<CarsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<car> cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<car>(entity =>
            {
                entity.Property(e => e.model).IsUnicode(false);
                entity.Property(e => e.plate).IsUnicode(false);
            });
        }
    }
}
