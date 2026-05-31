using FinanceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Budget> Budgets => Set<Budget>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Name).IsRequired().HasMaxLength(100);
            e.Property(c => c.Color).HasMaxLength(7);
        });

        modelBuilder.Entity<Transaction>(e =>
        {
            e.HasKey(t => t.Id);
            e.Property(t => t.Amount).HasPrecision(18, 2);
            e.Property(t => t.Description).HasMaxLength(300);
            e.HasOne(t => t.Category)
             .WithMany(c => c.Transactions)
             .HasForeignKey(t => t.CategoryId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Budget>(e =>
        {
            e.HasKey(b => b.Id);
            e.Property(b => b.PlannedAmount).HasPrecision(18, 2);
            e.HasIndex(b => new { b.CategoryId, b.Year, b.Month }).IsUnique();
            e.HasOne(b => b.Category)
             .WithMany(c => c.Budgets)
             .HasForeignKey(b => b.CategoryId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        SeedDefaultCategories(modelBuilder);
    }

    private static void SeedDefaultCategories(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Salário", Color = "#4CAF50", IsDefault = true },
            new Category { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Freelance", Color = "#8BC34A", IsDefault = true },
            new Category { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "Investimentos", Color = "#009688", IsDefault = true },
            new Category { Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "Moradia", Color = "#2196F3", IsDefault = true },
            new Category { Id = Guid.Parse("00000000-0000-0000-0000-000000000005"), Name = "Alimentação", Color = "#FF9800", IsDefault = true },
            new Category { Id = Guid.Parse("00000000-0000-0000-0000-000000000006"), Name = "Transporte", Color = "#FF5722", IsDefault = true },
            new Category { Id = Guid.Parse("00000000-0000-0000-0000-000000000007"), Name = "Saúde", Color = "#E91E63", IsDefault = true },
            new Category { Id = Guid.Parse("00000000-0000-0000-0000-000000000008"), Name = "Educação", Color = "#9C27B0", IsDefault = true },
            new Category { Id = Guid.Parse("00000000-0000-0000-0000-000000000009"), Name = "Lazer", Color = "#3F51B5", IsDefault = true },
            new Category { Id = Guid.Parse("00000000-0000-0000-0000-000000000010"), Name = "Vestuário", Color = "#00BCD4", IsDefault = true },
            new Category { Id = Guid.Parse("00000000-0000-0000-0000-000000000011"), Name = "Reserva de Emergência", Color = "#FFC107", IsDefault = true },
            new Category { Id = Guid.Parse("00000000-0000-0000-0000-000000000012"), Name = "Outros", Color = "#9E9E9E", IsDefault = true }
        );
    }
}
