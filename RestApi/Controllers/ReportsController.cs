using Core.Abstractions;

using Microsoft.AspNetCore.Mvc;

using PrimaryPorts.Models;

namespace ExpenseManagement.TransactionEntities.Controllers;


public class ReportsController : BaseController
{
    private readonly IReportsService _reportsService;
    
    public ReportsController(
        IReportsService reportsService)
    {
        _reportsService = reportsService;
    }
    
    [HttpPost("request-report")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400, Type = typeof(ValidationProblemDetails))]
    public async Task<IActionResult> RequestReportAsync([FromBody] ReportGenerationRequestData requestData)
    {
        await _reportsService.ProcessReportGenerationRequestAsync(requestData);
        
        return Ok("Report generation request has been successfully submitted.");
    }
}
