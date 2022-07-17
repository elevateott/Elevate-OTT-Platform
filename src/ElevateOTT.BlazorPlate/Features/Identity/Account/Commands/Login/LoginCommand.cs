namespace ElevateOTT.BlazorPlate.Features.Identity.Account.Commands.Login;

public class LoginCommand
{
    #region Public Properties

    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
    public string ReturnUrl { get; set; }

    #endregion Public Properties
}