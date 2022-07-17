namespace ElevateOTT.Domain.Entities.Identity;

public class ApplicationUserToken : IdentityUserToken<string>
{
    #region Public Properties

    public ApplicationUser User { get; set; }

    #endregion Public Properties
}