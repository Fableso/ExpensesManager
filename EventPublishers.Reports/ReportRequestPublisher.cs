using EventPublishers.Reports.Abstractions;

using SecondaryPorts.Abstractions;
using SecondaryPorts.Models;

namespace EventPublishers.Reports;

public class ReportRequestPublisher : IReportRequestPublisher
{
    private readonly IAmazonSimpleQueueServiceProvider _amazonSimpleQueueServiceProvider;
    
    public ReportRequestPublisher(IAmazonSimpleQueueServiceProvider amazonSimpleQueueServiceProvider)
    {
        _amazonSimpleQueueServiceProvider = amazonSimpleQueueServiceProvider;
    }
    
    public async Task PublishReportRequest(
        ReportGenerationRequestEntity reportGenerationRequest)
    {
        await _amazonSimpleQueueServiceProvider.Publish(
            reportGenerationRequest,
            "ReportGenerationRequest",
            1,
            Guid.NewGuid().ToString());
    }
}
