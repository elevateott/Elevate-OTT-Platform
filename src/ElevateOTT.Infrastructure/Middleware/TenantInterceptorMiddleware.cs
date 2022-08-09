namespace ElevateOTT.Infrastructure.Middleware;

public static class TenantInterceptorMiddlewareExtensions
{
    #region Public Methods

    public static IApplicationBuilder UseTenantInterceptor(this IApplicationBuilder builder,
        Action<MultiTenancyOptions> configureOptions)
    {
        var options = new MultiTenancyOptions();

        configureOptions(options);

        return builder.UseMiddleware<TenantInterceptorMiddleware>(options);
        //return builder.UseWhen(_ => options.TenantMode == TenantMode.MultiTenant, appBuilder => appBuilder.UseMiddleware<TenantHandlerMiddleware>(options));
    }

    #endregion Public Methods
}

public class TenantInterceptorMiddleware
{
    #region Private Fields

    private const string Host = "host";

    private readonly RequestDelegate _next;
    private readonly MultiTenancyOptions _options;

    #endregion Private Fields

    #region Public Constructors

    public TenantInterceptorMiddleware(RequestDelegate next, MultiTenancyOptions options)
    {
        // TODO use Guard 

        _next = next ?? throw new ArgumentNullException(nameof(next), nameof(next) + " is required");
        _options = options;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task InvokeAsync(HttpContext httpContext, IApplicationDbContext dbContext, ITenantResolver tenantResolver)
    {
        tenantResolver.TenantMode = _options.TenantMode;

        switch (_options.TenantMode)
        {
            case TenantMode.MultiTenant when httpContext == null:
                throw new ArgumentNullException(nameof(httpContext), nameof(httpContext) + " is required");

            case TenantMode.MultiTenant:
            {
                // If 'callback' in path, then request is from a webhook.
                // In webhook callbacks, tenants are set in the webhook services.
                if (httpContext.Request.Path.Value != null &&
                    httpContext.Request.Path.Value.Contains("callback")) break;

                var tenantName = httpContext.Request.Headers["X-Tenant"];

                if (tenantName.Count == 0)
                    tenantName = string.Empty;

                Console.WriteLine("tenantName @ API interceptor: " + tenantName);
                
                // TODO guard against tenantName null or empty

                // TODO check if tenant name is Name or CustomDomain

                var tenantId = dbContext.Tenants.FirstOrDefault(t => t.Name.Equals(tenantName.FirstOrDefault()) || t.CustomDomain.Equals(tenantName.FirstOrDefault()))?.Id;

                if (httpContext.Request.Path.Value is { } pathValue
                    && tenantId is null
                    && tenantName[0] != Host
                    && !pathValue.Contains("hangfire")
                    && !pathValue.Contains("/Hubs/")
                    && !pathValue.Contains("callback"))
                    throw new Exception(Resource.Invalid_tenant_name);

                tenantResolver.SetTenantId(tenantId);

                tenantResolver.SetTenantName(tenantName);

                break;
            }

            case TenantMode.SingleTenant:
                {
                    tenantResolver.SetTenantId(Guid.Empty);

                    tenantResolver.SetTenantName("Default");

                    break;
                }
        }

        await _next(httpContext);
    }

    #endregion Public Methods
}
