﻿namespace ElevateOTT.Domain.Entities.Identity;

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

    public Guid? TenantId { get; set; }
    public string? Name { get; set; } 
    public string? Surname { get; set; } 
    public string FullName => $"{Name} {Surname}";
    public string? JobTitle { get; set; } 
    public string? AvatarUri { get; set; } 
    public bool IsSuspended { get; set; }
    public bool IsStatic { get; set; }
    public bool IsDemo { get; set; }
    public string? RefreshToken { get; set; } 
    public DateTime RefreshTokenTimeSpan { get; set; }
    public List<ApplicationUserClaim> Claims { get; set; }
    public List<ApplicationUserLogin> Logins { get; set; }
    public List<ApplicationUserToken> Tokens { get; set; }
    public List<ApplicationUserRole> UserRoles { get; set; }
    public List<ApplicationUserAttachment> UserAttachments { get; set; }
    public string? CreatedBy { get; set; } 
    public DateTime CreatedOn { get; set; }
    public string? ModifiedBy { get; set; } 
    public DateTime? ModifiedOn { get; set; }
    public string? DeletedBy { get; set; } 
    public DateTime? DeletedOn { get; set; }

    #endregion Public Properties
}
