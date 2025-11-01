using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Domain;

namespace ShopTARge24.Data
{
    public class ShopTARge24Context : DbContext
    {
        public ShopTARge24Context(DbContextOptions<ShopTARge24Context> options)
            : base(options)
        {
        }

        public DbSet<Spaceships> Spaceships { get; set; }
        public DbSet<FileToApi> FileToApis { get; set; }
        public DbSet<RealEstate> RealEstates { get; set; }
        public DbSet<Kindergarten> Kindergartens { get; set; } = default!;
        public DbSet<KindergartenFile> KindergartenFiles { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<KindergartenFile>()
                .HasOne(kf => kf.Kindergarten)
                .WithMany(k => k.Files)
                .HasForeignKey(kf => kf.KindergartenId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
