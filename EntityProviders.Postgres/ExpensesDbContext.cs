using EntityProviders.Postgres.Configurations;
using EntityProviders.Postgres.Entities;

using Microsoft.EntityFrameworkCore;

namespace EntityProviders.Postgres;

public class ExpensesDbContext : DbContext
{
    public ExpensesDbContext(DbContextOptions<ExpensesDbContext> options)
        : base(options)
    {
    }

    public DbSet<Expense> Expenses { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
    }
}
