using Microsoft.EntityFrameworkCore;
using PROG7311_POE.Models;


namespace PROG7311_POE.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User table configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);
                entity.Property(u => u.UserId).ValueGeneratedOnAdd();
                entity.HasIndex(u => u.Username).IsUnique();
                entity.Property(u => u.Username).IsRequired();
                entity.Property(u => u.PasswordHash).IsRequired();
                entity.Property(u => u.Role).HasConversion<string>().IsRequired();
                entity.Property(u => u.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // Farmer table configuration
            modelBuilder.Entity<Farmer>(entity =>
            {
                entity.HasKey(f => f.FarmerId);
                entity.Property(f => f.FarmerId).ValueGeneratedOnAdd();
                entity.Property(f => f.FarmName).IsRequired();
                entity.Property(f => f.OwnerName).IsRequired();
                entity.Property(f => f.JoinedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                // One to one relationship with User
                entity.HasOne(f => f.User)
                    .WithOne(u => u.Farmer)
                    .HasForeignKey<Farmer>(f => f.UserId);
            });

            // ProductCategory table configuration
            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(pc => pc.CategoryId);
                entity.Property(pc => pc.CategoryId).ValueGeneratedOnAdd();
                entity.HasIndex(pc => pc.CategoryName).IsUnique();
                entity.Property(pc => pc.CategoryName).IsRequired();
            });

            // Product table configuration
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.ProductId);
                entity.Property(p => p.ProductId).ValueGeneratedOnAdd();
                entity.Property(p => p.ProductName).IsRequired();
                entity.Property(p => p.ProductionDate).IsRequired();
                entity.Property(p => p.QuantityAvailable).HasColumnType("DECIMAL(10,2)").IsRequired();
                entity.Property(p => p.UnitOfMeasure).IsRequired();
                entity.Property(p => p.Price).HasColumnType("DECIMAL(10,2)");
                entity.Property(p => p.IsOrganic).HasDefaultValue(false);
                entity.Property(p => p.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                // Many to one relationship with Farmer
                entity.HasOne(p => p.Farmer)
                    .WithMany(f => f.Products)
                    .HasForeignKey(p => p.FarmerId);

                // Many to one relationship with ProductCategory
                entity.HasOne(p => p.Category)
                    .WithMany(pc => pc.Products)
                    .HasForeignKey(p => p.CategoryId);
            });
        }
    }
}
