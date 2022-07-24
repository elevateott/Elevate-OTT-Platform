namespace ElevateOTT.Domain.Entities.Settings.IdentitySettings;

public class UserSettings : ISettingsSchema, IMayHaveTenant
{
    #region Public Properties

    public Guid Id { get; set; }
    public string AllowedUserNameCharacters { get; set; } = string.Empty;
    public bool NewUsersActiveByDefault { get; set; }
    public Guid? TenantId { get; set; }

    #endregion Public Properties
}
