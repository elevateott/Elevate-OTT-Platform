namespace BinaryPlate.Application.Common.Extensions.Identity;

public class ApplicationRoleManager : RoleManager<ApplicationRole>
{
    #region Public Constructors

    public ApplicationRoleManager(IRoleStore<ApplicationRole> store,
                                  IEnumerable<IRoleValidator<ApplicationRole>> roleValidators,
                                  ILookupNormalizer keyNormalizer,
                                  IdentityErrorDescriber errors,
                                  ILogger<RoleManager<ApplicationRole>> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
    {
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task AddOrRemoveRolePermission(IList<Guid> assignedRolePermissionIds, ApplicationRole dbRole, IApplicationDbContext dbContext)
    {
        if (assignedRolePermissionIds != null)
        {
            var applicationPermissionsList = await dbContext.ApplicationPermissions.ToListAsync();

            var addedRolePermissions = assignedRolePermissionIds.Where(arp => dbRole.RoleClaims.All(rc => rc.ClaimValue != applicationPermissionsList.FirstOrDefault(c => c.Id == arp)?.Name)).ToList();

            var removedRolePermissions = dbRole.RoleClaims.Where(rc => assignedRolePermissionIds.All(arp => arp != applicationPermissionsList.FirstOrDefault(c => c.Name == rc.ClaimValue)?.Id)).ToList();

            var selectedPermissions = dbContext.ApplicationPermissions.Where(p => addedRolePermissions.Contains(p.Id));

            foreach (var addedRolePermission in selectedPermissions)
            {
                dbRole.RoleClaims.Add(new ApplicationRoleClaim()
                {
                    ClaimType = "permissions",
                    ClaimValue = addedRolePermission.Name
                });
            }

            foreach (var removedRolePermission in removedRolePermissions)
            {
                dbRole.RoleClaims.Remove(removedRolePermission);
            }

            var userClaimsTObeRemoved = await RemoveUserExcludedOrphanPermissions(dbContext, removedRolePermissions);

            dbContext.UserClaims.RemoveRange(userClaimsTObeRemoved);
        }
        else
        {
            dbRole.RoleClaims.Clear();
        }
    }

    public async Task RemoveExcludedPermissionsFromAllUsers(ApplicationRole role, IApplicationDbContext dbContext)
    {
        var allUsersClaims = await dbContext.UserClaims.ToListAsync();
        var removedExcludedUserPermissions = (from c in allUsersClaims
                                              join r in role.RoleClaims on c.ClaimValue equals r.ClaimValue
                                              where c.IsExcluded
                                              select c).ToList();

        dbContext.UserClaims.RemoveRange(removedExcludedUserPermissions);
        await dbContext.SaveChangesAsync();
    }

    #endregion Public Methods

    #region Private Methods

    private static async Task<IEnumerable<ApplicationUserClaim>> RemoveUserExcludedOrphanPermissions(IApplicationDbContext dbContext, List<ApplicationRoleClaim> removedRolePermissions)
    {
        var userClaims = await dbContext.UserClaims.ToListAsync();
        var userClaimsTObeRemoved = userClaims
            .Where(uc => removedRolePermissions.Any(rc => rc.ClaimValue == uc.ClaimValue && uc.IsExcluded));
        return userClaimsTObeRemoved;
    }

    #endregion Private Methods
}