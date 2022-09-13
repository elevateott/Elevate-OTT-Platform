namespace ElevateOTT.Infrastructure.Identity.Extensions;

public static class AppSettingsExtensions
{
    #region Public Methods

    public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppOptions>(configuration.GetSection(AppOptions.Section));
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.Section));
        services.Configure<SmtpOption>(configuration.GetSection(SmtpOption.Section));
        services.Configure<ClientAppOptions>(configuration.GetSection(ClientAppOptions.Section));
        services.Configure<LicenseInfoOptions>(configuration.GetSection(LicenseInfoOptions.Section));
        services.Configure<BlobOptions>(configuration.GetSection(BlobOptions.Section));
        services.Configure<ChargebeeOptions>(configuration.GetSection(ChargebeeOptions.Section));
        services.Configure<MuxOptions>(configuration.GetSection(MuxOptions.Section));
        services.Configure<CryptoOptions>(configuration.GetSection(CryptoOptions.Section));
        services.Configure<TinyPNGOptions>(configuration.GetSection(TinyPNGOptions.Section));
        return services;
    }

    #endregion Public Methods
}
