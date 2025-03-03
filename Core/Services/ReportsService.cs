using AutoMapper;

using Core.Abstractions;

using Microsoft.AspNetCore.Http;

using PrimaryPorts.Models;

using SecondaryPorts.Abstractions;
using SecondaryPorts.Models;

namespace Core.Services;

public class ReportsService : BaseService, IReportsService
{
    private readonly IReportRequestPublisher _reportRequestPublisher;
    private readonly IMapper _mapper;
    private readonly long _userId;
    private readonly string _userEmail;
    
    public ReportsService(
        IHttpContextAccessor httpContextAccessor,
        IReportRequestPublisher reportRequestPublisher,
        IMapper mapper) : base(httpContextAccessor)
    {
        _reportRequestPublisher = reportRequestPublisher;
        _mapper = mapper;
        _userId = GetUserId();
        _userEmail = GetUserEmail();
    }
    
    public async Task ProcessReportGenerationRequestAsync(ReportGenerationRequestData requestData)
    {
        var reportGenerationRequestEntity = _mapper.Map<ReportGenerationRequestEntity>(requestData);
        
        reportGenerationRequestEntity.UserId = _userId;
        reportGenerationRequestEntity.Email = _userEmail;
        
        await _reportRequestPublisher.PublishReportRequest(reportGenerationRequestEntity);
    }
}
