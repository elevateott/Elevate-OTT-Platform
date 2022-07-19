namespace ElevateOTT.ClientPortal;

public class Program
{
    #region Public Methods

    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.RootComponents.Add<App>("#app");

        builder.Services.AddScoped<SpinnerService>();

        builder.Services.AddHttpClientInterceptor();

        builder.Services.AddScoped<HttpInterceptorService>();

        builder.Services.AddScoped<IApiUrlProvider, ApiUrlProvider>();

        builder.Services.AddHttpClient("HttpInterceptorService");

        builder.Services.AddScoped(sp => new HttpClient
        {
            BaseAddress = new Uri(builder.Configuration.GetSection("BaseApiUrl").Value)
        }.EnableIntercept(sp));

        ConfigureServices(builder.Services);

        var host = builder.Build();

        var localStorage = host.Services.GetRequiredService<ILocalStorageService>();

        var culture = await localStorage.GetItemAsync<string>("Culture");

        var selectedCulture = culture == null ? new CultureInfo("en-US") : new CultureInfo(culture);

        //var selectedCulture = culture; You can uncomment this line and comment the above line.

        CultureInfo.DefaultThreadCurrentCulture = selectedCulture;

        CultureInfo.DefaultThreadCurrentUICulture = selectedCulture;

        await host.RunAsync();
    }

    #endregion Public Methods

    #region Private Methods

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddOptions();

        services.AddSingleton<ILocalizationService, LocalizationService>();

        services.AddScoped<IAccessTokenProvider, AccessTokenProvider>();

        services.AddScoped<IAuthenticationService, AuthenticationService>();

        services.AddScoped<AuthStateProvider>();

        services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

        services.AddScoped<IRefreshTokenService, RefreshTokenService>();

        services.AddScoped<IReturnUrlProvider, ReturnUrlProvider>();

        services.AddSingleton<IAppStateManager, AppStateManager>();

        services.AddSingleton<INavigationService, NavigationService>();

        services.AddSingleton<IBreadcrumbService, BreadcrumbService>();

        services.AddScoped<IHttpService, HttpService>();

        services.AddScoped<IAccountsClient, AccountsClient>();

        services.AddScoped<IManageClient, ManageClient>();

        services.AddScoped<IRolesClient, RolesClient>();

        services.AddScoped<IPermissionsClient, PermissionsClient>();

        services.AddScoped<IUsersClient, UsersClient>();

        services.AddScoped<IAppSettingsClient, AppSettingsClient>();

        services.AddScoped<IApplicantsClient, ApplicantsClient>();

        services.AddScoped<IReportsClient, ReportsClient>();

        services.AddScoped<IDashboardClient, DashboardClient>();

        services.AddLocalization();

        services.AddBlazoredLocalStorage();

        services.AddMudServices(config =>
        {
            config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
            config.SnackbarConfiguration.PreventDuplicates = true;
            config.SnackbarConfiguration.NewestOnTop = true;
            config.SnackbarConfiguration.ShowCloseIcon = true;
            config.SnackbarConfiguration.VisibleStateDuration = 6000;
            config.SnackbarConfiguration.HideTransitionDuration = 500;
            config.SnackbarConfiguration.ShowTransitionDuration = 500;
            config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
        });

        services.AddAuthorizationCore();
    }

    #endregion Private Methods
}