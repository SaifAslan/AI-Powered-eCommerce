using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<Colour> Colour { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<ProductVariant> ProductVariant { get; set; }

        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin"  ,
                    NormalizedName = "ADMIN"
                },
                new IdentityRole {
                    Name = "User",
                    NormalizedName = "USER"
                }
            };

            builder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);  // Precision: 18, Scale: 2

            builder.Entity<ProductMaterial>()
                .Property(pm => pm.Percentage)
                .HasPrecision(5, 2);  // Adjust precision and scale as necessary
            builder.Entity<IdentityRole>().HasData(roles);

            //ProductMaterial relationships
            builder.Entity<ProductMaterial>().HasKey(pm => new { pm.ProductId, pm.MaterialId });
            builder.Entity<ProductMaterial>()
                .HasOne(pm => pm.Product)
                .WithMany(p => p.ProductMaterials)
                .HasForeignKey(pm => pm.ProductId);
            builder.Entity<ProductMaterial>()
                .HasOne(pm => pm.Material)
                .WithMany(m => m.ProductMaterials)
                .HasForeignKey(pm => pm.MaterialId);

            // ProductVariant relationships
            builder.Entity<Product>()
            .HasMany(p => p.Variants)
            .WithOne(v => v.Product)
            .HasForeignKey(v => v.ProductId);

            //Variant-Size relationships
            builder.Entity<ProductVariant>()
                .HasOne(pv => pv.Size)
                .WithMany()
                .HasForeignKey(pv => pv.SizeId);

            //Variant-Color relationships
            builder.Entity<ProductVariant>()
                .HasOne(pv => pv.Colour)
                .WithMany()
                .HasForeignKey(pv => pv.ColourId);
        }
    }
}
