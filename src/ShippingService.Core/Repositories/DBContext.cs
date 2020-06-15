using Microsoft.EntityFrameworkCore;
using ShippingService.Core.Models;

namespace ShippingService.Core
{
    public partial class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<express> express { get; set; }
        public virtual DbSet<bulk> bulk { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<express>(entity =>
            {
                entity.Property(e => e.id).IsUnicode(false);
                entity.Property(e => e.type).IsUnicode(false);
                entity.Property(e => e.trackable).IsUnicode(false);
                entity.Property(e => e.service_level).IsUnicode(false);
                entity.Property(e => e.country).IsUnicode(false);
                entity.Property(e => e.country_code).IsUnicode(false);
                entity.Property(e => e.rate_flag).IsUnicode(false);
                entity.Property(e => e.weight).IsUnicode(false);
                entity.Property(e => e.dhl_express).IsUnicode(false);
                entity.Property(e => e.sf_economy).IsUnicode(false);
                entity.Property(e => e.zone).IsUnicode(false);
            });

            modelBuilder.Entity<bulk>(entity =>
            {
                entity.Property(e => e.id).IsUnicode(false);
                entity.Property(e => e.type).IsUnicode(false);
                entity.Property(e => e.trackable).IsUnicode(false);
                entity.Property(e => e.service_level).IsUnicode(false);
                entity.Property(e => e.country).IsUnicode(false);
                entity.Property(e => e.country_code).IsUnicode(false);
                entity.Property(e => e.item_weight).IsUnicode(false);
                entity.Property(e => e.ascendia_item_rate).IsUnicode(false);
                entity.Property(e => e.ascendia_rate_per_kg).IsUnicode(false);
                entity.Property(e => e.singpost_item_rate).IsUnicode(false);
                entity.Property(e => e.singpost_rate_per_kg).IsUnicode(false);
                entity.Property(e => e.dai_item_rate).IsUnicode(false);
                entity.Property(e => e.dai_rate_per_kg).IsUnicode(false);
            });
        }
    }
}
