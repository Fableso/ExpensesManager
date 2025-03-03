using EntityProviders.Postgres.Mapping;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using SecondaryPorts.Abstractions;

namespace EntityProviders.Postgres.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPostgresEntityProviders(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<ExpensesDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        
        services.AddAutoMapper(mapperConfigurationExpression =>
        {
            mapperConfigurationExpression.AddProfile<MappingProfile>();
        });

        services.AddScoped<IExpensesProvider, ExpensesProvider>();

        return services;
    }
}
