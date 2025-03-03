using Microsoft.AspNetCore.Http;

namespace Core.Services;

public abstract class BaseService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string UserIdClaimType = "userId";
    private const string UserEmailClaimType = "userEmail";

    protected BaseService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    protected long GetUserId()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user?.Identity?.IsAuthenticated == true)
        {
            return long.Parse(user.FindFirst(UserIdClaimType)?.Value);
        }
        
        throw new UnauthorizedAccessException("User is not authenticated");
    }
    
    protected string GetUserEmail()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user?.Identity?.IsAuthenticated == true)
        {
            return user.FindFirst(UserEmailClaimType)?.Value;
        }
        
        throw new UnauthorizedAccessException("User is not authenticated");
    }
}
