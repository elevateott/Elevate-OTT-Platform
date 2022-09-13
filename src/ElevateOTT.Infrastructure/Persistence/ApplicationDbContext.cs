﻿using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<
        ApplicationUser,
        ApplicationRole,
        string,
        ApplicationUserClaim,
        ApplicationUserRole,
        ApplicationUserLogin,
        ApplicationRoleClaim,
        ApplicationUserToken>,
    IApplicationDbContext
{
    #region Private Fields

    private static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder =>
          {
              builder.AddConsole();
          });

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITenantResolver _tenantResolver;
    private readonly IConfiguration _configuration;

    #endregion Private Fields

    #region Public Constructors
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
                                IHttpContextAccessor httpContextAccessor,
                                ITenantResolver tenantResolver,
                                IConfiguration configuration) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
        _tenantResolver = tenantResolver;
        _configuration = configuration;
        Current = this;
    }

    #endregion Public Constructors

    #region Public Properties

    public DbContext Current { get; }

    public override DbSet<ApplicationUserRole>? UserRoles { get; set; }
    public override DbSet<ApplicationUserClaim>? UserClaims { get; set; }
    public override DbSet<ApplicationUserLogin>? UserLogins { get; set; }
    public override DbSet<ApplicationRoleClaim>? RoleClaims { get; set; }
    public override DbSet<ApplicationUserToken>? UserTokens { get; set; }

    public DbSet<ApplicationUserAttachment>? ApplicationUserAttachments { get; set; }
    public DbSet<ApplicationPermission>? ApplicationPermissions { get; set; }

    public DbSet<UserSettings>? UserSettings { get; set; }
    public DbSet<PasswordSettings>? PasswordSettings { get; set; }
    public DbSet<LockoutSettings>? LockoutSettings { get; set; }
    public DbSet<SignInSettings>? SignInSettings { get; set; }
    public DbSet<TokenSettings>? TokenSettings { get; set; }
    public DbSet<FileStorageSettings>? FileStorageSettings { get; set; }

    public DbSet<Tenant>? Tenants { get; set; }


    public DbSet<Applicant>? Applicants { get; set; }
    public DbSet<Reference>? References { get; set; }
    public DbSet<Report>? Reports { get; set; }

    public DbSet<AuthorModel>? Authors { get; set; }
    public DbSet<VideoModel>? Videos { get; set; }
    public DbSet<LiveStreamModel>? LiveStreams { get; set; }
    public DbSet<PodcastModel>? Podcasts { get; set; }
    public DbSet<CategoryModel>? Categories { get; set; }
    public DbSet<VideoCategoryModel>? VideosCategories { get; set; }
    public DbSet<ContentFeedModel>? ContentFeeds { get; set; }

    // TODO update interface when adding entities here

    #endregion Public Properties

    #region Private Properties

    private Guid? TargetTenantIdProvidedByHost { get; set; }

    #endregion Private Properties

    #region Public Methods

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            var validationContext = new ValidationContext(entry);
            Validator.ValidateObject(entry, validationContext);
        }

        var userId = _httpContextAccessor.GetUserId();
        var utcNow = DateTimeProvider.UtcNow;

        foreach (var entry in ChangeTracker.Entries<IAuditable>())
        {
            switch (entry)
            {
                case { State: EntityState.Added }:
                    entry.Property("CreatedOn").CurrentValue = utcNow;
                    entry.Property("CreatedBy").CurrentValue = userId;
                    break;

                case { State: EntityState.Modified }:
                    entry.Property("ModifiedOn").CurrentValue = utcNow;
                    entry.Property("ModifiedBy").CurrentValue = userId;
                    break;

                case { State: EntityState.Deleted }:
                    if (entry.Entity is ISoftDeletable)
                    {
                        entry.Property("DeletedOn").CurrentValue = utcNow;
                        entry.Property("DeletedBy").CurrentValue = userId;
                    }
                    break;
            }
        }

        var currentTenantId = _tenantResolver.GetTenantId();

        if (_tenantResolver.TenantMode == TenantMode.MultiTenant)
        {
            foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>())
            {
                switch (entry)
                {
                    case { State: EntityState.Added }:
                        entry.Property("TenantId").CurrentValue = ThrowExceptionIfNull(currentTenantId);
                        break;

                    case { State: EntityState.Modified }:
                        entry.Property("TenantId").CurrentValue ??= ThrowExceptionIfNull(currentTenantId);
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<IMayHaveTenant>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Property("TenantId").CurrentValue = TargetTenantIdProvidedByHost ?? currentTenantId;
                        break;

                    case EntityState.Modified:
                        entry.Property("TenantId").CurrentValue = TargetTenantIdProvidedByHost ?? currentTenantId;
                        break;
                }
            }
        }

        foreach (var entry in ChangeTracker.Entries<ISoftDeletable>().Where(x => x.State == EntityState.Deleted))
        {
            // Set the entity to unchanged (if we mark the whole entity as Modified, every field
            // gets sent to Db as an update)
            entry.State = EntityState.Unchanged;
            // Only update the IsDeleted flag - only this will get sent to the Db
            entry.Property("IsDeleted").CurrentValue = true;
        }

        try
        {
            var id = await base.SaveChangesAsync(cancellationToken);
            TargetTenantIdProvidedByHost = null;
            return id;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.InnerException?.Message ?? ex.ToString());
        }
    }

    public void SetTargetTenantId(Guid tenantId)
    {
        TargetTenantIdProvidedByHost = tenantId;
    }

    #endregion Public Methods

    #region Protected Methods

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(_loggerFactory);
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureEntitiesBasedOnMultiTenantMode(modelBuilder);

        ConfigureSoftDeletableEntities(modelBuilder);

        ConfigureSettingsSchemaEntities(modelBuilder);

        ConfigureOneToManyRelationships(modelBuilder);

        ConfigureManyToManyRelationships(modelBuilder);

        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    #endregion Protected Methods

    #region Private Methods

    private static void IgnoreMappingIMayHaveTenant(ModelBuilder modelBuilder)
    {
        var propertyNames = typeof(IMayHaveTenant).GetProperties()
            .Select(p => p.Name)
            .ToList();

        var entityTypes = modelBuilder.Model.GetEntityTypes()
            .Where(t => typeof(IMayHaveTenant).IsAssignableFrom(t.ClrType));

        foreach (var entityType in entityTypes)
        {
            var entityTypeBuilder = modelBuilder.Entity(entityType.ClrType);
            foreach (var propertyName in propertyNames)
                entityTypeBuilder.Ignore(propertyName);
        }
    }

    private static void IgnoreMappingIMustHaveTenant(ModelBuilder modelBuilder)
    {
        var propertyNames = typeof(IMustHaveTenant).GetProperties()
            .Select(p => p.Name)
            .ToList();

        var entityTypes = modelBuilder.Model.GetEntityTypes()
            .Where(t => typeof(IMustHaveTenant).IsAssignableFrom(t.ClrType));

        foreach (var entityType in entityTypes)
        {
            var entityTypeBuilder = modelBuilder.Entity(entityType.ClrType);
            foreach (var propertyName in propertyNames)
                entityTypeBuilder.Ignore(propertyName);
        }
    }

    private static void ConfigureSettingsSchemaEntities(ModelBuilder modelBuilder)
    {
        // Creating SQL database schema for the settings tables
        foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(e => typeof(ISettingsSchema).IsAssignableFrom(e.ClrType)))
            modelBuilder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name, "Settings");
    }

    private static void ConfigureMultiTenantsEntities(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes().Where(e => typeof(IMustHaveTenant).IsAssignableFrom(e.ClrType)))
            builder.Entity(entityType.ClrType).Property<Guid>("TenantId").IsRequired();

        foreach (var entityType in builder.Model.GetEntityTypes().Where(e => typeof(IMayHaveTenant).IsAssignableFrom(e.ClrType)))
            builder.Entity(entityType.ClrType).Property<Guid?>("TenantId").IsRequired(false);
    }

    private static void ConfigureSoftDeletableEntities(ModelBuilder builder)
    {
        // Creating navigation or shadow properties for all entity

        foreach (var entityType in builder.Model.GetEntityTypes().Where(e => typeof(ISoftDeletable).IsAssignableFrom(e.ClrType)))
            builder.Entity(entityType.ClrType).Property<bool>("IsDeleted").IsRequired();

        builder.SetQueryFilterOnAllEntities<ISoftDeletable>(p => EF.Property<bool>(p, "IsDeleted") == false);
    }

    private static void ConfigureOneToManyRelationships(ModelBuilder modelBuilder)
    {
        // Creating one-to-many relationships where ClientSetNull required
        modelBuilder.Entity<VideoModel>()
            .HasOne(p => p.Author)
            .WithMany(b => b.Videos)
            .OnDelete(DeleteBehavior.ClientSetNull);

        modelBuilder.Entity<PodcastModel>()
            .HasOne(p => p.Author)
            .WithMany(b => b.Podcasts)
            .OnDelete(DeleteBehavior.ClientSetNull);

        modelBuilder.Entity<LiveStreamModel>()
            .HasOne(p => p.Author)
            .WithMany(b => b.LiveStreams)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }

    private static void ConfigureManyToManyRelationships(ModelBuilder modelBuilder)
    {
        // Creating many-to-many relationships
        modelBuilder.Entity<VideoCategoryModel>().HasKey(x => new { x.VideoId, x.CategoryId });
        //modelBuilder.Entity<VideoCollectionModel>().HasKey(x => new { x.VideoId, x.CollectionId });
        //modelBuilder.Entity<LiveStreamCategoryModel>().HasKey(x => new { x.LiveStreamId, x.CategoryId });
        //modelBuilder.Entity<PodcastCategoryModel>().HasKey(x => new { x.PodcastId, x.CategoryId });
        //modelBuilder.Entity<PodcastCollectionModel>().HasKey(x => new { x.PodcastId, x.CollectionId });
        //modelBuilder.Entity<CategoryCollectionModel>().HasKey(x => new { x.CategoryId, x.CollectionId });
        //modelBuilder.Entity<VideoTagModel>().HasKey(x => new { x.VideoId, x.TagId });
    }

    private void InitiateTenantMode()
    {
        var tenantMode = (TenantMode)Enum.Parse(typeof(TenantMode), _configuration.GetValue<string>("AppOptions:TenantModeOptions"), true);
        _tenantResolver.TenantMode = tenantMode;
    }

    private void ConfigureEntitiesBasedOnMultiTenantMode(ModelBuilder modelBuilder)
    {
        InitiateTenantMode();

        switch (_tenantResolver.TenantMode)
        {
            case TenantMode.MultiTenant:
                ConfigureMultiTenantsEntities(modelBuilder);

                SetQueryFilterOnMultiTenantsEntities(modelBuilder);

                break;

            case TenantMode.SingleTenant:
            default:
                IgnoreMappingIMustHaveTenant(modelBuilder);

                IgnoreMappingIMayHaveTenant(modelBuilder);
                break;
        }
    }

    private Guid? ThrowExceptionIfNull(Guid? tenantId)
    {
        if (tenantId != null)
            return tenantId;

        if (TargetTenantIdProvidedByHost != null)
            return TargetTenantIdProvidedByHost;

        throw new Exception("IMustHaveTenant entity must have tenant Id.");
    }

    private void SetQueryFilterOnMultiTenantsEntities(ModelBuilder builder)
    {
        builder.SetQueryFilterOnAllEntities<IMayHaveTenant>(p => p.TenantId == _tenantResolver.GetTenantId());

        builder.SetQueryFilterOnAllEntities<IMustHaveTenant>(p => p.TenantId == _tenantResolver.GetTenantId());
    }

    #endregion Private Methods
}
