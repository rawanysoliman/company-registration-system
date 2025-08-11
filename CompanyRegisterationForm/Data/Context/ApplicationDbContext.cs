using Microsoft.EntityFrameworkCore;
using CompanyRegisterationForm.Data.Entities;

namespace CompanyRegisterationForm.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                
                entity.Property(e => e.ArabicName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.EnglishName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
                entity.Property(e => e.WebsiteUrl).HasMaxLength(500);
                entity.Property(e => e.LogoPath).HasMaxLength(500);
                
                entity.HasIndex(e => e.Email).IsUnique();
            });
        }
    }
} 