namespace ElevateOTT.ClientPortal.Services;

public class HttpInterceptorService : IDisposable
{
    #region Private Fields
    private readonly HttpClient _httpClient;
    private readonly HttpClientInterceptor _httpClientInterceptor;
    private readonly SpinnerService _spinnerService;
    private readonly NavigationManager _navigationManager;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IAuthenticationService _authenticationService;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    #endregion Private Fields

    #region Public Constructors

    public HttpInterceptorService(IHttpClientFactory httpClientFactory,
                                  HttpClientInterceptor httpClientInterceptor,
                                  SpinnerService spinnerService,
                                  NavigationManager navigationManager,
                                  IRefreshTokenService refreshTokenService,
                                  IAuthenticationService authenticationService, 
                                  AuthenticationStateProvider authenticationStateProvider)
    {
        _httpClient = httpClientFactory.CreateClient("HttpInterceptorService");
        _httpClientInterceptor = httpClientInterceptor;
        _spinnerService = spinnerService;
        _navigationManager = navigationManager;
        _refreshTokenService = refreshTokenService;
        _authenticationService = authenticationService;
        _authenticationStateProvider = authenticationStateProvider;
        _httpClientInterceptor.BeforeSend += async (s, e) => await HttpClientInterceptor_BeforeSendAsync(s, e);
        _httpClientInterceptor.AfterSend += async (s, e) => await HttpClientInterceptor_AfterSendAsync(s, e);
    }

    #endregion Public Constructors

    #region Public Methods

    public void Dispose()
    {
        _httpClientInterceptor.BeforeSend -= async (s, e) => await HttpClientInterceptor_BeforeSendAsync(s, e);
        _httpClientInterceptor.AfterSend -= async (s, e) => await HttpClientInterceptor_AfterSendAsync(s, e);
        _httpClient.Dispose();
    }

    #endregion Public Methods

    #region Private Methods

    private async Task HttpClientInterceptor_BeforeSendAsync(object sender, HttpClientInterceptorEventArgs e)
    {
        _spinnerService.Show();

        //
        // TODO 
        // Get tenant id from claim and add to header instead if tenant name
        //
        var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var tenantIdClaim = authenticationState.User.Claims.FirstOrDefault(x => x.Type.Equals("TenantId"));
        string tenantId = tenantIdClaim is not null ? tenantIdClaim.Value : string.Empty;
        Console.WriteLine($"tenantId: {tenantId}");


        string tenantName = _navigationManager.GetSubDomain();

        Console.WriteLine($"tenantName: {tenantName}");

        e.Request.Headers.Add("X-Tenant", tenantName);

        if (e.Request.Headers.Authorization != null)
        {
            var absPath = e.Request.RequestUri.AbsolutePath;
            if (!absPath.Contains("/api/account/"))
            {
                var token = await _refreshTokenService.TryRefreshToken();
                if (!string.IsNullOrEmpty(token))
                    e.Request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
        }
    }

    private async Task HttpClientInterceptor_AfterSendAsync(object sender, HttpClientInterceptorEventArgs e)
    {
        _spinnerService.Hide();
        if (e.Response is { StatusCode: HttpStatusCode.Unauthorized })
            await _authenticationService.Logout();

        await Task.CompletedTask;
    }

    #endregion Private Methods
}
