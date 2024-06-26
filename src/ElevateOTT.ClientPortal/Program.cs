﻿using ElevateOTT.ClientPortal.Hubs;
using Syncfusion.Blazor;

namespace ElevateOTT.ClientPortal;

public class Program
{
    #region Public Methods

    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.RootComponents.Add<App>("#app");

        Syncfusion.Licensing.SyncfusionLicenseProvider
            .RegisterLicense(builder.Configuration["SyncfusionLicenseKey"]);

        // Syncfusion
        builder.Services.AddSyncfusionBlazor();

        builder.Services.AddScoped<SpinnerService>();

        builder.Services.AddHttpClientInterceptor();

        builder.Services.AddScoped<HttpInterceptorService>();

        builder.Services.AddScoped<IApiUrlProvider, ApiUrlProvider>();

        builder.Services.AddHttpClient("HttpInterceptorService");

        builder.Services.AddHttpClient("ApiUrl", (sp, client) =>
        {
            client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseApiUrl"));
            client.EnableIntercept(sp);
        });

        builder.Services.AddHttpClient("HubUrl", (sp, client) =>
        {
            client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseHubUrl"));
            client.EnableIntercept(sp);
        });

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

        services.AddScoped<IAuthorsClient, AuthorsClient>();

        services.AddScoped<IVideosClient, VideosClient>();

        services.AddScoped<ICategoriesClient, CategoriesClient>();

        services.AddScoped<VideoHub>();

        services.AddScoped<LiveStreamHub>();

        services.AddScoped<ChatHub>();

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
