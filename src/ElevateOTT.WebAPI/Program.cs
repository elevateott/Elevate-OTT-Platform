var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);

builder.Services.AddHealthChecks();

builder.Services.AddAppLocalization();

builder.Services
    .AddControllers(options => { options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true; })
    .AddFluentValidation(fv => { fv.RegisterValidatorsFromAssemblyContaining<IApplicationDbContext>(); })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

builder.Services.Configure<FormOptions>(x =>
{
    x.ValueLengthLimit = 1073741824;
    x.MultipartBodyLengthLimit = 1073741824; // In case of multipart
});

builder.Services.AddSwaggerApi();

builder.Services.AddAuth(builder.Configuration);

//builder.Services.AddHttpsRedirection(options =>
//{
//    options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
//    options.HttpsPort = 44388;
//});

builder.Services.AddCors(options =>
{
    //var tenantMode = (TenantMode)Enum.Parse(typeof(TenantMode), builder.Configuration.GetValue<string>("AppOptions:TenantModeOptions"));
    //switch (tenantMode)
    //{
    //    case TenantMode.SingleTenant:
    //        options.AddPolicy("CorsPolicy", policyBuilder => policyBuilder.WithOrigins(builder.Configuration.GetValue<string>("ClientApp:SingleTenantHostName"))
    //                              .SetIsOriginAllowedToAllowWildcardSubdomains()
    //                              .AllowAnyMethod()
    //                              .AllowAnyHeader()
    //                              .AllowCredentials());
    //        break;
    //    case TenantMode.MultiTenant:
    //        options.AddPolicy("CorsPolicy", policyBuilder => policyBuilder
    //                  .WithOrigins(builder.Configuration.GetValue<string>("ClientApp:MultiTenantHostName"))
    //                  .SetIsOriginAllowedToAllowWildcardSubdomains()
    //                  .AllowAnyMethod()
    //                  .AllowAnyHeader()
    //                  .AllowCredentials());
    //        break;
    //}

    options.AddPolicy("CorsPolicy", policyBuilder => policyBuilder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

builder.Services.AddSignalR();

builder.Services.AddSingleton<TimerManager>();

builder.Services.AddScoped<IHubNotificationService, HubNotificationService>();

builder.Services.AddScoped<ISignalRContextProvider, SignalRContextProvider>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync();
        var permissionScannerService = services.GetRequiredService<IPermissionScannerService>();
        await ApplicationDbContextSeeder.SeedAsync(permissionScannerService);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        logger.LogError(ex, $"An error occurred while migrating or seeding the database.|{ex.InnerException?.ToString() ?? ex.Message}");
    }
}
// Configure the HTTP request pipeline.

ServiceActivator.Configure(app.Services.CreateScope().ServiceProvider);

app.UseHealthChecks("/health");

//app.UseHttpsRedirection();

app.UseDefaultFiles();

app.UseStaticFiles();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAppLocalization();

app.UseSwaggerApi();

app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions
{
    IsApiOnly = false,
    UseApiProblemDetailsException = true,
    ShowStatusCode = true,
    WrapWhenApiPathStartsWith = "/api",
    ExcludePaths = new[]
    {
        new AutoWrapperExcludePath("/DashboardHub/.*|/DashboardHub", ExcludeMode.Regex)
    }
});

app.UseTenantInterceptor(options =>
{
    options.TenantMode = (TenantMode)Enum.Parse(typeof(TenantMode),
        builder.Configuration.GetValue<string>("AppOptions:TenantModeOptions"));
});

app.UseIdentityOptions();

app.UseAuth();


// If you want to access the hangfire dashboard from outside the localhost,
// please refer to this link. https://docs.hangfire.io/en/latest/configuration/using-dashboard.html
app.UseHangfireDashboard();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<DashboardHub>("Hubs/DashboardHub");
    endpoints.MapHub<DataExportHub>("Hubs/DataExportHub");
});

app.Run();
