using EntityProviders.Postgres.Abstractions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityProviders.Postgres.Extensions;


public static class EntityTypeBuilderExtensions
{
    public static EntityTypeBuilder<TEntity> WithAuditableFields<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, IAuditableTimestamps
    {
        builder
            .Property(e => e.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

        return builder;
    }
}

