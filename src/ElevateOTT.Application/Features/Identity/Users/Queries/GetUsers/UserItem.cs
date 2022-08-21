using ElevateOTT.Domain.Common.DTOs;

namespace ElevateOTT.Application.Features.Identity.Users.Queries.GetUsers;

public class UserItem : AuditableDto
{
    #region Public Properties

    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
    public string AvatarUri { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; }
    public bool IsSuspended { get; set; }

    #endregion Public Properties

    #region Public Methods

    public static UserItem MapFromEntity(ApplicationUser u)
    {
        return new()
        {
            Id = u.Id,
            UserName = u.UserName,
            Email = u.Email,
            Name = u.Name,
            FullName = u.FullName,
            Surname = u.Surname,
            JobTitle = u.JobTitle,
            AvatarUri = u.AvatarUri,
            IsSuspended = u.IsSuspended,
            EmailConfirmed = u.EmailConfirmed,
            CreatedOn = u.CreatedOn,
            CreatedBy = u.CreatedBy,
            ModifiedOn = u.ModifiedOn,
            ModifiedBy = u.ModifiedBy
        };
    }

    #endregion Public Methods
}
