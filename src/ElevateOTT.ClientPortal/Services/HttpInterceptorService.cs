using ElevateOTT.ClientPortal.Exceptions;
using Microsoft.AspNetCore.Components;

namespace ElevateOTT.ClientPortal.Services;

// ref: https://github.com/jsakamoto/Toolbelt.Blazor.HttpClientInterceptor

public class HttpInterceptorService : IDisposable
{
    #region Private Fields
    private readonly HttpClient _httpClient;
    private readonly HttpClientInterceptor _httpClientInterceptor;
    private readonly SpinnerService _spinnerService;
    private readonly NavigationManager _navigationManager;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IAuthenticationService _authenticationService;
    private readonly ILocalStorageService _localStorageService;
    #endregion Private Fields

    #region Public Constructors

    public HttpInterceptorService(IHttpClientFactory httpClientFactory,
                                  HttpClientInterceptor httpClientInterceptor,
                                  SpinnerService spinnerService,
                                  NavigationManager navigationManager,
                                  IRefreshTokenService refreshTokenService,
                                  IAuthenticationService authenticationService, ILocalStorageService localStorageService)
    {
        _httpClient = httpClientFactory.CreateClient("HttpInterceptorService");
        _httpClientInterceptor = httpClientInterceptor;
        _spinnerService = spinnerService;
        _navigationManager = navigationManager;
        _refreshTokenService = refreshTokenService;
        _authenticationService = authenticationService;
        _localStorageService = localStorageService;
        _httpClientInterceptor.BeforeSendAsync += HttpClientInterceptor_BeforeSendAsync;
        _httpClientInterceptor.AfterSendAsync += HttpClientInterceptor_AfterSendAsync;
    }

    #endregion Public Constructors

    #region Public Methods

    public void Dispose()
    {
        _httpClientInterceptor.BeforeSendAsync -= HttpClientInterceptor_BeforeSendAsync;
        _httpClientInterceptor.AfterSendAsync -= HttpClientInterceptor_AfterSendAsync;
        _httpClient.Dispose();
    }

    #endregion Public Methods

    #region Private Methods

    private async Task HttpClientInterceptor_BeforeSendAsync(object sender, HttpClientInterceptorEventArgs e)
    {
        _spinnerService.Show();

        //
        // TODO
        // Use this once we start using single sub domain
        //
        var tenantId = await _localStorageService.GetItemAsync<string>(Constants.TenantIdStorageKey);

        Console.WriteLine($"tenantId: {tenantId}");

        string tenantName = _navigationManager.GetSubDomain();
        string domainName = _navigationManager.GetDomain();

        _navigationManager.Test();

        Console.WriteLine($"tenantName: {tenantName}");
        Console.WriteLine($"domainName: {domainName}");


        e.Request.Headers.Add("X-Tenant", tenantName);

        // Console.WriteLine(new System.Diagnostics.StackTrace());


        // Console.WriteLine($"e.Request.Headers: {e.Request.Headers}");

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

        if (e.Response == null)
        {
            _navigationManager.NavigateTo("/pages/error/server-error");
            throw new HttpResponseException("Server not available");
        }

        if (!e.Response.IsSuccessStatusCode)
        {
            string message = string.Empty;
            var responseCode = e.Response.StatusCode;

            // TODO new svgs for error pages
            // TODO test error handling


            switch (responseCode)
            {
                case HttpStatusCode.NotFound:
                    message = Resource.Error_404_Message;
                    _navigationManager.NavigateTo($"/pages/error/404/{message}");
                    break;
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                    message = Resource.Error_401_Message;
                    _navigationManager.NavigateTo($"/pages/error/unauthorized/{message}");
                    break;
                default:
                    message = Resource.Error_500_Message;
                    _navigationManager.NavigateTo($"/pages/error/server-error/{message}");
                    break;
            }

            Console.WriteLine(message);
            throw new HttpResponseException(message);
        }

        await Task.CompletedTask;
    }

    #endregion Private Methods
}
