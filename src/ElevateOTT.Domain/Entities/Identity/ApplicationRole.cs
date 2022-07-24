namespace ElevateOTT.Domain.Entities.Identity;

public class ApplicationRole : IdentityRole, IAuditable, IMayHaveTenant
{
    #region Public Constructors

    public ApplicationRole()
    {
        UserRoles = new List<ApplicationUserRole>();
        RoleClaims = new List<ApplicationRoleClaim>();
    }

    #endregion Public Constructors

    #region Public Properties

    public bool IsStatic { get; set; }
    public bool IsDefault { get; set; }
    public Guid? TenantId { get; set; }
    public ICollection<ApplicationUserRole> UserRoles { get; set; }
    public ICollection<ApplicationRoleClaim> RoleClaims { get; set; }

    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; } = string.Empty;
    public DateTime? ModifiedOn { get; set; }
    public string DeletedBy { get; set; } = string.Empty;
    public DateTime? DeletedOn { get; set; }

    #endregion Public Properties
}
