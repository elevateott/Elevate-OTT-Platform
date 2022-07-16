namespace BinaryPlate.Application.Common.Helpers.Validators;

public class MultiTenantUserValidator : IUserValidator<ApplicationUser>
{
    #region Public Methods

    public async Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
    {
        using var serviceScope = ServiceActivator.GetScope() ?? throw new ArgumentNullException("ServiceActivator.GetScope()");

        var httpContextAccessor = serviceScope.ServiceProvider.GetService<IHttpContextAccessor>();

        var tenantResolver = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<ITenantResolver>();

        var userInterfaces = typeof(ApplicationUser).GetInterfaces();

        ThrowExceptionIfNotEligibleForMultitenancy(tenantResolver, userInterfaces);

        TrySetTenantId(user, userInterfaces, tenantResolver);

        var isAddOperation = await manager.FindByIdAsync(user.Id) == null;

        var combinationExists = await CheckCombinationExists(manager, user, isAddOperation, tenantResolver);

        return combinationExists
            ? IdentityResult.Failed(new IdentityError
            {
                Code = "DuplicateUserName",
                Description = tenantResolver.TenantMode == TenantMode.MultiTenant
                    ? Resource.The_specified_username_and_email_are_already_registered_in_the_given_tenant
                    : Resource.The_specified_username_and_email_are_already_registered
            })
            : IdentityResult.Success;
    }

    private static void TrySetTenantId(ApplicationUser user, Type[] userInterfaces, ITenantResolver tenantResolver)
    {
        if (userInterfaces.Any(i => !i.IsGenericType && i.Name == nameof(IMustHaveTenant)))
        {
            var tenantId = tenantResolver.GetTenantId() ??
                           throw new ArgumentNullException("Unable to assign null value to User.TenantId.");
            var propertyInfo = user.GetType().GetProperty("TenantId");
            if (propertyInfo != null)
                propertyInfo.SetValue(user, Convert.ChangeType(tenantId, propertyInfo.PropertyType), null);
        }
        else if (userInterfaces.Any(i => !i.IsGenericType && i.Name == nameof(IMayHaveTenant)))
        {
            var tenantId = tenantResolver.GetTenantId();
            var propertyInfo = user.GetType().GetProperty("TenantId");
            if (propertyInfo != null && propertyInfo.GetValue(user) == null)
            {
                var type = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                var safeValue = (tenantId == null) ? null : Convert.ChangeType(tenantId, type);
                propertyInfo.SetValue(user, safeValue, null);
            }
        }
    }

    private static void ThrowExceptionIfNotEligibleForMultitenancy(ITenantResolver tenantResolver, Type[] userInterfaces)
    {
        if (tenantResolver.TenantMode == TenantMode.MultiTenant &&
            !userInterfaces.Any(i => i.Name is nameof(IMustHaveTenant) or nameof(IMayHaveTenant)))
            throw new ArgumentException("ApplicationUser must implement either IMustHaveTenant or IMayHaveTenant.");
    }

    #endregion Public Methods

    #region Private Methods

    private static async Task<bool> CheckCombinationExists(UserManager<ApplicationUser> manager,
        ApplicationUser user,
        bool isAddOperation,
        ITenantResolver tenantResolver)
    {
        bool combinationExists;
        if (isAddOperation)
        {
            combinationExists = tenantResolver.TenantMode switch
            {
                TenantMode.MultiTenant => await manager.Users
                    .AnyAsync(u => u.NormalizedEmail == user.UserName.ToUpper()
                                   && u.NormalizedEmail == user.Email.ToUpper()
                                   && EF.Property<ApplicationUser>(u, "TenantId") == user.GetType().GetProperty("TenantId").GetValue(user)),

                TenantMode.SingleTenant => await manager.Users.AnyAsync(u =>
                    u.NormalizedEmail == user.UserName.ToUpper() && u.NormalizedEmail == user.Email.ToUpper()),

                _ => throw new ArgumentOutOfRangeException(Resource.Please_specify_the_application_tenant_mode)
            };
        }
        else
        {
            combinationExists = tenantResolver.TenantMode switch
            {
                TenantMode.MultiTenant => await manager.Users.Where(u => u.Id != user.Id && EF.Property<ApplicationUser>(u, "TenantId") != null)
                    .AnyAsync(u => u.NormalizedEmail == user.UserName.ToUpper()
                                   && u.NormalizedEmail == user.Email.ToUpper()
                                   && EF.Property<ApplicationUser>(u, "TenantId") == user.GetType().GetProperty("TenantId").GetValue(user)),

                TenantMode.SingleTenant => await manager.Users.Where(u => u.Id != user.Id)
                    .AnyAsync(
                        u => u.NormalizedEmail == user.UserName.ToUpper() && u.NormalizedEmail == user.Email.ToUpper()),

                _ => throw new ArgumentOutOfRangeException(Resource.Please_specify_the_application_tenant_mode)
            };
        }

        return combinationExists;
    }

    #endregion Private Methods
}