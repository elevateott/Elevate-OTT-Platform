namespace ElevateOTT.Application.UseCases.Identity;

public class TenantUseCase : ITenantUseCase
{
    #region Private Fields

    private readonly IApplicationDbContext _dbContext;
    private readonly ApplicationPartManager _partManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IDemoIdentitySeeder _demoIdentitySeeder;
    private readonly ITenantResolver _tenantResolver;

    #endregion Private Fields

    #region Public Constructors

    public TenantUseCase(IApplicationDbContext dbContext,
                         ApplicationPartManager partManager,
                         UserManager<ApplicationUser> userManager,
                         RoleManager<ApplicationRole> roleManager,
                         IDemoIdentitySeeder demoIdentitySeeder,
                         ITenantResolver tenantResolver)
    {
        _dbContext = dbContext;
        _partManager = partManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _demoIdentitySeeder = demoIdentitySeeder;
        _tenantResolver = tenantResolver;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<Envelope<CreateTenantResponse>> AddTenant(CreateTenantCommand request)
    {
        if (_tenantResolver.TenantMode != TenantMode.MultiTenant)
            return Envelope<CreateTenantResponse>.Result.ServerError(Resource.Unable_to_create_new_tenant_in_single_tenant_mode);

        var tenant = request.MapToEntity();

        await _dbContext.Tenants.AddAsync(tenant);

        await _dbContext.SaveChangesAsync();

        var payload = new CreateTenantResponse
        {
            Id = tenant.Id,
            SuccessMessage = Resource.Tenant_has_been_created_successfully
        };

        _tenantResolver.SetTenantId(tenant.Id);
        await CreateSampleApplicants();
        await AddStaticRoles();

        var result = await CreateTenantSuperAdmin();

        if (result.IsError)
            return Envelope<CreateTenantResponse>.Result.AddErrors(result.ModelStateErrors, ResponseType.ServerError, result.Message);

        result = await _demoIdentitySeeder.SeedDemoOfficersUsers();

        return result.IsError
            ? Envelope<CreateTenantResponse>.Result.ServerError(result.Message)
            : Envelope<CreateTenantResponse>.Result.Ok(payload);
    }

    #endregion Public Methods

    #region Private Methods

    private async Task CreateSampleApplicants()
    {
        var rnd = new Random(100000000);
        for (var a = 0; a <= 20; a++)
        {
            var referencesList = new Collection<Reference>();

            for (var r = 0; r <= 15; r++)
            {
                var reference = new Reference()
                {
                    Name = rnd.Next().ToString(),
                    JobTitle = rnd.Next().ToString(),
                    Phone = rnd.Next().ToString(),
                };
                referencesList.Add(reference);
            }

            var applicant = new Applicant
            {
                Ssn = rnd.Next(100000000, 999999999),
                FirstName = rnd.Next().ToString(),
                LastName = rnd.Next().ToString(),
                DateOfBirth = new DateTime(1999, 10, 11),
                Height = 180,
                Weight = 80,
                References = referencesList
            };

            _dbContext.Applicants.Add(applicant);
        }
        await _dbContext.SaveChangesAsync();
    }

    private async Task<Envelope<ApplicationUser>> CreateTenantSuperAdmin()
    {
        var adminUser = new ApplicationUser
        {
            Email = "admin@demo",
            UserName = "admin@demo",
            Name = "Marcella",
            Surname = "Wallace",
            JobTitle = "Administrator",
            EmailConfirmed = true,
            IsSuspended = false,
            IsDemo = true,
        };

        var result = await _userManager.CreateAsync(adminUser, "123456");

        if (!result.Succeeded)
            return Envelope<ApplicationUser>.Result.AddErrors(result.Errors.ToApplicationResult(), ResponseType.BadRequest);

        var adminRole = await _roleManager.FindByNameAsync("Admin");

        if (adminRole != null)
            result = await _userManager.AddToRoleAsync(adminUser, adminRole.Name);

        return !result.Succeeded ? Envelope<ApplicationUser>.Result.AddErrors(result.Errors.ToApplicationResult(), ResponseType.BadRequest) : Envelope<ApplicationUser>.Result.Ok(adminUser);
    }

    private async Task AddStaticRoles()
    {
        var adminRole = new ApplicationRole
        {
            Name = "Admin",
            IsStatic = true
        };

        var userRole = new ApplicationRole
        {
            Name = "User",
            IsStatic = true
        };

        var auditorRole = new ApplicationRole
        {
            Name = "Auditor",
            IsStatic = true
        };

        var accountantRole = new ApplicationRole
        {
            Name = "Accountant",
            IsStatic = true
        };

        var ceoRole = new ApplicationRole
        {
            Name = "CEO",
            IsStatic = true
        };

        var driverRole = new ApplicationRole
        {
            Name = "Driver",
            IsStatic = true
        };

        await GrantAllPermissionsForAdminRole(adminRole);

        if (!await _roleManager.RoleExistsAsync(adminRole.Name))
            await _roleManager.CreateAsync(adminRole);

        if (!await _roleManager.RoleExistsAsync(userRole.Name))
            await _roleManager.CreateAsync(userRole);

        if (!await _roleManager.RoleExistsAsync(auditorRole.Name))
            await _roleManager.CreateAsync(auditorRole);

        if (!await _roleManager.RoleExistsAsync(accountantRole.Name))
            await _roleManager.CreateAsync(accountantRole);

        if (!await _roleManager.RoleExistsAsync(ceoRole.Name))
            await _roleManager.CreateAsync(ceoRole);

        if (!await _roleManager.RoleExistsAsync(driverRole.Name))
            await _roleManager.CreateAsync(driverRole);
    }

    private async Task GrantAllPermissionsForAdminRole(ApplicationRole adminRole)
    {
        var allPermissions = await _dbContext.ApplicationPermissions.ToListAsync();

        foreach (var permission in allPermissions)
        {
            adminRole.RoleClaims.Add(new ApplicationRoleClaim
            {
                ClaimType = "permissions",
                ClaimValue = permission.Name
            });
        }
    }

    #endregion Private Methods
}