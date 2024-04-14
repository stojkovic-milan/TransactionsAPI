using System.Security.Claims;
using TransactionsAPI.Application.Common.Interfaces;

namespace TransactionsAPI.WebUI.Services;

public class HeaderPropertyAccessService : IHeaderPropertyAccessService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HeaderPropertyAccessService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetPropertyValue(string propertyName)
    {
        return _httpContextAccessor.HttpContext?.Request.Headers[propertyName];
    }
}