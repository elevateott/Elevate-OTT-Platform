using System.ComponentModel.DataAnnotations;

namespace ElevateOTT.ClientPortal.Features.Identity.Account.Commands.Register;

public class RegisterCommand
{
    #region Public Properties

    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
    public string? ReturnUrl { get; set; }
    public string? ChannelName { get; set; }
    public string? HeardAboutUsFrom { get; set; }
    public string? SubDomain { get; set; }
    public string? CustomDomain { get; set; }

    #endregion Public Properties
}
