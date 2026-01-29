using Microsoft.EntityFrameworkCore;
using FamilyBudgetExpenseTracker.Models;

namespace FamilyBudgetExpenseTracker.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Budget> Budgets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships
        modelBuilder.Entity<User>()
            .HasMany(u => u.Expenses)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Category>()
            .HasMany(c => c.Expenses)
            .WithOne(e => e.Category)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}