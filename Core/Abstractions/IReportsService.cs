using PrimaryPorts.Models;

using SecondaryPorts.Abstractions;

namespace Core.Abstractions;

public interface IReportsService
{
    Task ProcessReportGenerationRequestAsync(ReportGenerationRequestData requestData);
}
