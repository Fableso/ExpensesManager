using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.TransactionEntities.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class BaseController : ControllerBase
{
}
