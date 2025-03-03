using EventPublishers.Reports.Abstractions;

using Microsoft.Extensions.DependencyInjection;

using SecondaryPorts.Abstractions;

namespace EventPublishers.Reports.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddReportsEventPublishers(
        this IServiceCollection services,
        string regionEndpoint,
        string queueUrl)
    {
        services.AddSingleton<IAmazonSimpleQueueServiceProvider>(_ => new AmazonSimpleQueueServiceProvider(regionEndpoint, queueUrl));
        services.AddSingleton<IReportRequestPublisher, ReportRequestPublisher>();

        return services;
    }
}
