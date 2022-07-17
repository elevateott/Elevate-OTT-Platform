namespace ElevateOTT.BlazorPlate.Features.Identity.Account.Commands.LoginWithRecoveryCode;

public class LoginWithRecoveryCodeCommand
{
    #region Public Properties

    public string RecoveryCode { get; set; }
    public string UserName { get; set; }
    public string ReturnUrl { get; set; }

    #endregion Public Properties
}