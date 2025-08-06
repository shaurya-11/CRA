using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PatchManager.Models;

namespace PatchManager.Data
{
    public class PatchDbContext : DbContext
    {
        public PatchDbContext(DbContextOptions<PatchDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Patch> Patches { get; set; }
        public DbSet<PatchStatus> PatchStatuses { get; set; }

        public DbSet<Admin> Admins { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relationships
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Customer)
                .HasForeignKey(p => p.CustomerId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Patches)
                .WithOne(pa => pa.Product)
                .HasForeignKey(pa => pa.ProductId);

            modelBuilder.Entity<PatchStatus>()
                .HasOne(ps => ps.Customer)
                .WithMany(c => c.PatchStatuses)
                .HasForeignKey(ps => ps.CustomerId);

            modelBuilder.Entity<PatchStatus>()
                .HasOne(ps => ps.Patch)
                .WithMany(p => p.PatchStatuses)
                .HasForeignKey(ps => ps.PatchId);

            modelBuilder.Entity<PatchStatus>()
                .Property(ps => ps.Status)
                .HasConversion(new EnumToStringConverter<PatchStatusEnum>());

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,

                    ServerIp = "192.168.1.10",
                    Username = "customer_a",
                    PasswordHash = "ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad"
                },
                new Customer
                {
                    Id = 2,

                    ServerIp = "192.168.1.11",
                    Username = "customer_b",
                    PasswordHash = "ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad"
                }
            );

            modelBuilder.Entity<Admin>().HasData(
    new Admin
    {
        Id = 1,
        Username = "admin",
        PasswordHash = "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4"
    });


            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Product A",
                    CustomerId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Product B",
                    CustomerId = 2
                }
            );

            modelBuilder.Entity<Patch>().HasData(
                new Patch
                {
                    Id = 1,
                    Version = "1.0.0",
                    FileName = "patch_1.0.0.zip",
                    Description = "Initial release",
                    ReleasedOn = new DateTime(2025, 07, 01),

                    ProductId = 1
                },
                new Patch
                {
                    Id = 2,
                    Version = "1.1.0",
                    FileName = "patch_1.1.0.zip",
                    Description = "Minor bug fixes",
                    ReleasedOn = new DateTime(2025, 07, 15),

                    ProductId = 2
                }
            );

            modelBuilder.Entity<PatchStatus>().HasData(
                new PatchStatus
                {
                    Id = 1,
                    CustomerId = 1,
                    PatchId = 1,
                    Status = PatchStatusEnum.Installed,
                    UpdatedAt = new DateTime(2025, 07, 10)s
                },
                new PatchStatus
                {
                    Id = 2,
                    CustomerId = 2,
                    PatchId = 2,
                    Status = PatchStatusEnum.Pending,
                    UpdatedAt = new DateTime(2025, 09, 10)
                }
            );
        }
    }
}
