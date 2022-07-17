namespace ElevateOTT.BlazorPlate.Features.Identity.Account.Commands.LoginWith2fa;

public class LoginWith2FaCommand
{
    #region Public Properties

    public string UserName { get; set; }
    public string TwoFactorCode { get; set; }
    public bool RememberMachine { get; set; }
    public bool RememberMe { get; set; }

    #endregion Public Properties
}