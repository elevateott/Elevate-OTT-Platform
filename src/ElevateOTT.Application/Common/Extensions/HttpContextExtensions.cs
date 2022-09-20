namespace ElevateOTT.Application.Common.Extensions;

public static class HttpContextExtensions
{
    #region Public Methods

    public static async Task<string> GetUserNameAsync(this IHttpContextAccessor httpContextAccessor)
    {
        if (httpContextAccessor.HttpContext != null)
        {
            var userManager = httpContextAccessor.HttpContext.RequestServices.GetService<UserManager<ApplicationUser>>();

            var userId = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var currentUser = await userManager.FindByIdAsync(userId);

            if (currentUser?.UserName != null) return currentUser.UserName;
        }
        return string.Empty;
    }

    public static string GetUserId(this IHttpContextAccessor httpContextAccessor)
    {
        var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        return userId ?? string.Empty;
    }

    public static string GetLanguage(this IHttpContextAccessor httpContextAccessor)
    {
        var language = httpContextAccessor.HttpContext.Request.Headers["Accept-LanguageCode"].ToString();

        return language ?? string.Empty;
    }

    public static string GetTenantFromRequestHeader(this IHttpContextAccessor httpContextAccessor)
    {
        var tenantName = httpContextAccessor.HttpContext.Request.Headers["X-Tenant"];

        return (tenantName.Count == 0) ? string.Empty : tenantName;
    }

    public static string GetClientAppHost(this IHttpContextAccessor httpContextAccessor)
    {
        var configReaderService = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IConfigReaderService>();
        var tenantResolver = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<ITenantResolver>();
        var clientAppOptions = configReaderService.GetClientAppOptions();

        return tenantResolver.TenantMode switch
        {
            TenantMode.MultiTenant => string.Format(clientAppOptions.MultiTenantHostName, configReaderService.GetSubDomain()),

            _ => clientAppOptions.SingleTenantHostName
        };
    }

    #endregion Public Methods
}