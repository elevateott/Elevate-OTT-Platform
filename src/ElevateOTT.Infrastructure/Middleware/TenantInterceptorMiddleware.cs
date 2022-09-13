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

                // If registration workflow, then no tenant exists yet.
                // Tenant created before app user in registration workflow.
                if (httpContext.Request.Path.Value != null &&
                      httpContext.Request.Path.Value.ToLower().Contains("account/register")) break;

                // If login workflow, then tenant not yet known
                if (httpContext.Request.Path.Value != null &&
                    httpContext.Request.Path.Value.ToLower().Contains("account/login")) break;

                // X-Tenant could be tenant id guid or subdomain/domain.
                // If request from client portal, X-Tenant is tenant id guid
                // If request from streaming web app, X-Tenant is subdomain/domain

                var xTenant = httpContext.Request.Headers["X-Tenant"];

                if (xTenant.Count == 0)
                    xTenant = string.Empty;

                if (dbContext.Tenants != null)
                {
                    Guid? tenantId = Guid.TryParse(xTenant.FirstOrDefault(), out Guid xTenantValue) 
                        ? xTenantValue 
                        : dbContext.Tenants.FirstOrDefault(t => t.CustomDomain != null && t.SubDomain != null && (t.SubDomain.Equals(xTenant.FirstOrDefault()) || t.CustomDomain.Equals(xTenant.FirstOrDefault())))?.Id;

                    Console.WriteLine("tenantName @ API interceptor: " + xTenant);
                
                    // TODO guard against tenantName null or empty
                    // TODO check if tenant name is Name or CustomDomain
                    //tenantId = Guid.Parse("58330475-6dd1-47a0-bc22-3afa1cb0ece8");
                
                    if (httpContext.Request.Path.Value is { } pathValue
                        && tenantId is null
                        && xTenant[0] != Host
                        && !pathValue.Contains("hangfire")
                        && !pathValue.Contains("/Hubs/")
                        && !pathValue.Contains("callback")
                        && !pathValue.ToLower().Contains("account/register"))
                        throw new Exception(Resource.Invalid_tenant_name);

                    if (tenantId.HasValue)
                    {
                        tenantResolver.SetTenantId(tenantId);
                    }
                }

                //tenantResolver.SetTenantName(xTenant);

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
