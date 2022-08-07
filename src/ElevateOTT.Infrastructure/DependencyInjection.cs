﻿using ElevateOTT.Application.Common.Interfaces.Mux;
using ElevateOTT.Application.Common.Interfaces.Repository;
using ElevateOTT.Application.Common.Interfaces.UseCases.Content;
using ElevateOTT.Application.UseCases.Content;
using ElevateOTT.Infrastructure.Repository;
using ElevateOTT.Infrastructure.Services.Mux;

namespace ElevateOTT.Infrastructure;

public static class DependencyInjection
{
    #region Public Methods

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddHangfire(globalConfiguration => globalConfiguration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            }));

        services.AddHangfireServer();

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
            .AddDefaultTokenProviders()
            .AddPasswordValidator<CustomPasswordValidator<ApplicationUser>>()
            .AddUserManager<ApplicationUserManager>()
            .AddRoleManager<ApplicationRoleManager>()
            //.AddUserStore<CustomUserStore>()
            //.AddRoleStore<CustomRoleStore>()
            ;

        services.Replace(ServiceDescriptor.Scoped<IUserValidator<ApplicationUser>, MultiTenantUserValidator>());
        services.Replace(ServiceDescriptor.Scoped<IRoleValidator<ApplicationRole>, MultiTenantRoleValidator>());

        #region AlterDefaultPasswordHashingMethod

        //services.Configure<PasswordHasherOptions>(options =>
        //{
        //    options.IterationCount = 10000;
        //});

        #endregion AlterDefaultPasswordHashingMethod

        #region AnotherPasswordHashingMethod

        //services.AddScoped<IPasswordHasher<ApplicationUser>, BCryptPasswordHasher<ApplicationUser>>();
        //services.Configure<BCryptPasswordHasherOptions>(options =>
        //{
        //    options.WorkFactor = 10;
        //    options.EnhancedEntropy = false;
        //});

        #endregion AnotherPasswordHashingMethod

        // X-CSRF-Token
        services.AddAntiforgery(options =>
        {
            options.HeaderName = "X-XSRF-Token";
            options.SuppressXFrameOptionsHeader = false;
        });

        services.AddHttpContextAccessor();
        services.AddAppSettings(configuration);

        services.AddScoped<IdentityErrorDescriber, LocalizedIdentityErrorDescriber>();
        services.AddScoped<IStorageProvider, StorageProvider>();
        services.AddScoped<IStorageFactory, StorageFactory>();
        services.AddScoped<ITenantResolver, TenantResolver>();
        services.AddScoped<IDemoIdentitySeeder, DemoIdentitySeeder>();
        services.AddScoped<IReportingService, ReportingService>();
        services.AddScoped<IHtmlReportBuilder, HtmlReportBuilder>();

        services.AddScoped<IBlobStorageService, AzureStorageService>();
        services.AddScoped<IFileStorageService, OnPremiseStorageService>();
        services.AddScoped<IDataExportService, DataExportService>();
        services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
        services.AddScoped<IConfigReaderService, ConfigReaderService>();
        services.AddScoped<IPermissionScannerService, PermissionScannerService>();
        services.AddScoped<IAppSettingsUseCase, AppSettingsUseCase>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IDemoUserPasswordSetterService, DemoUserPasswordSetterService>();

         services.AddScoped<IMuxWebhookService, MuxWebhookService>();
         services.AddScoped<IMuxAssetService, MuxAssetService>();

         services.AddScoped<IAccountUseCase, AccountUseCase>();
        services.AddScoped<IManageUseCase, ManageUseCase>();
        services.AddScoped<IRoleUseCase, RoleUseCase>();
        services.AddScoped<IPermissionUseCase, PermissionUseCase>();
        services.AddScoped<IUserUseCase, UserUseCase>();
        services.AddScoped<ITenantUseCase, TenantUseCase>();
        services.AddScoped<IApplicantUseCase, ApplicantUseCase>();
        services.AddScoped<IReportUseCase, ReportUseCase>();

        services.AddScoped<IAuthorUseCase, AuthorUseCase>();
        services.AddScoped<IVideoUseCase, VideoUseCase>();

        services.AddScoped<IRepositoryManager, RepositoryManager>();

        return services;
    }

    #endregion Public Methods
}
