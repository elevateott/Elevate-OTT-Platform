namespace ElevateOTT.ClientPortal.Services;

public class AuthenticationService : IAuthenticationService
{
    #region Private Fields

    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly ILocalStorageService _localStorageService;

    #endregion Private Fields

    #region Public Constructors

    public AuthenticationService(HttpClient httpClient,
                                 AuthenticationStateProvider authStateProvider,
                                 ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _authStateProvider = authStateProvider;
        _localStorageService = localStorageService;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task Login(AuthResponse authResponse)
    {
        await Logout();

        await _localStorageService.SetItemAsync(TokenType.AccessToken, authResponse.AccessToken);

        await _localStorageService.SetItemAsync(TokenType.RefreshToken, authResponse.RefreshToken);

        await StoreTenantId();

        ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(authResponse.AccessToken);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authResponse.AccessToken);
    }

    public async Task ReAuthenticate(AuthResponse authResponse)
    {
        await CleanUp();

        await _localStorageService.SetItemAsync(TokenType.AccessToken, authResponse.AccessToken);

        await _localStorageService.SetItemAsync(TokenType.RefreshToken, authResponse.RefreshToken);

        await StoreTenantId();

        ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(authResponse.AccessToken);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authResponse.AccessToken);
    }

    public async Task Logout()
    {
        var authState = await ((AuthStateProvider)_authStateProvider).GetAuthenticationStateAsync();

        var user = authState.User;

        if (user.Identity is { IsAuthenticated: true })
        {
            await _localStorageService.RemoveItemAsync(TokenType.AccessToken);
            await _localStorageService.RemoveItemAsync(TokenType.RefreshToken);
            await _localStorageService.RemoveItemAsync(Constants.TenantIdStorageKey);
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }

    #endregion Public Methods

    #region Private Methods

    private async Task CleanUp()
    {
        await _localStorageService.RemoveItemAsync(TokenType.AccessToken);

        await _localStorageService.RemoveItemAsync(TokenType.RefreshToken);

        await _localStorageService.RemoveItemAsync(Constants.TenantIdStorageKey);

        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    private async Task StoreTenantId()
    {
        var authenticationState = await _authStateProvider.GetAuthenticationStateAsync();
        var tenantIdClaim = authenticationState.User.Claims.FirstOrDefault(x => x.Type.Equals("TenantId"));
        string tenantId = tenantIdClaim is not null ? tenantIdClaim.Value : string.Empty;

        await _localStorageService.SetItemAsync(Constants.TenantIdStorageKey, tenantId);
    }

    #endregion Private Methods
}
