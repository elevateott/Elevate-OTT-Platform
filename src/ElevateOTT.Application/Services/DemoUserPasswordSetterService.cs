namespace ElevateOTT.Application.Services;

public class DemoUserPasswordSetterService : IDemoUserPasswordSetterService
{
    #region Private Fields

    private readonly UserManager<ApplicationUser> _userManager;

    #endregion Private Fields

    #region Public Constructors

    public DemoUserPasswordSetterService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task ResetDefaultPassword(string currentPassword, ApplicationUser user)
    {
        if (user.IsDemo)
            await _userManager.ChangePasswordAsync(user, currentPassword, "123456");
    }

    #endregion Public Methods
}