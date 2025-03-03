namespace EntityProviders.Postgres.Abstractions;

public interface IAuditableTimestamps
{
    public DateTime CreatedAt { get; set; }
}
