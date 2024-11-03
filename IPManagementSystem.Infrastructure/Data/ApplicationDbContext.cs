using IPManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace IPManagementSystem.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Country> Countries { get; set; }
        public DbSet<IpAddressEntity> IPAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configs to Country
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.TwoLetterCode).IsRequired().HasMaxLength(2);
                entity.Property(e => e.ThreeLetterCode).IsRequired().HasMaxLength(3);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            // Configs to IPAddress
            modelBuilder.Entity<IpAddressEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.IP).IsRequired().HasMaxLength(15);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(e => e.Country)
                      .WithMany(c => c.IPAddresses)
                      .HasForeignKey(e => e.CountryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
