using SecondaryPorts.Models;

namespace SecondaryPorts.Abstractions;

public interface IReportRequestPublisher
{
    Task PublishReportRequest(ReportGenerationRequestEntity reportGenerationRequest);
}
