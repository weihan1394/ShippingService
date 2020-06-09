using Microsoft.EntityFrameworkCore;
using ShippingService.Core.Models;

namespace ShippingService.Core
{
    public partial class ShippingExpressContext : DbContext
    {
        public ShippingExpressContext(DbContextOptions<ShippingExpressContext> options)
            : base(options)
        {
        }

        public virtual DbSet<express> express { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<express>(entity =>
            {
                entity.Property(e => e.Type).IsUnicode(false);
                entity.Property(e => e.Trackable).IsUnicode(false);
                entity.Property(e => e.ServiceLevel).IsUnicode(false);
                entity.Property(e => e.Country).IsUnicode(false);
                entity.Property(e => e.CountryCode).IsUnicode(false);
                entity.Property(e => e.RateFlag).IsUnicode(false);
                entity.Property(e => e.Weight).IsUnicode(false);
                entity.Property(e => e.DHLExpress).IsUnicode(false);
                entity.Property(e => e.SFEconomy).IsUnicode(false);
                entity.Property(e => e.Zone).IsUnicode(false);
            });
        }
    }
}
