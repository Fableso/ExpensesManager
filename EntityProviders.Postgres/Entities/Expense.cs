using EntityProviders.Postgres.Abstractions;

namespace EntityProviders.Postgres.Entities;

public class Expense : IAuditableTimestamps
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string Category { get; set; }

    public decimal Amount { get; set; }
    
    public DateTime Date { get; set; }

    public DateTime CreatedAt { get; set; }
}