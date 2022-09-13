namespace ElevateOTT.Domain.Entities.Identity;

public class ApplicationUser : IdentityUser, IAuditable, IMayHaveTenant
{
    #region Public Constructors

    public ApplicationUser()
    {
        Claims = new List<ApplicationUserClaim>();
        Logins = new List<ApplicationUserLogin>();
        Tokens = new List<ApplicationUserToken>();
        UserRoles = new List<ApplicationUserRole>();
        UserAttachments = new List<ApplicationUserAttachment>();
    }

    #endregion Public Constructors

    #region Public Properties

    // TODO remove first/last name add FullName

    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
    public string AvatarUri { get; set; } = string.Empty;
    public bool IsSuspended { get; set; }
    public bool IsStatic { get; set; }
    public bool IsDemo { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenTimeSpan { get; set; }
    public Guid? TenantId { get; set; }
    public List<ApplicationUserClaim> Claims { get; set; }
    public List<ApplicationUserLogin> Logins { get; set; }
    public List<ApplicationUserToken> Tokens { get; set; }
    public List<ApplicationUserRole> UserRoles { get; set; }
    public List<ApplicationUserAttachment> UserAttachments { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; } = string.Empty;
    public DateTime? ModifiedOn { get; set; }
    public string DeletedBy { get; set; } = string.Empty;
    public DateTime? DeletedOn { get; set; }

    #endregion Public Properties
}
