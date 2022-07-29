namespace ElevateOTT.Application.Common.Models.ApplicationOptions;

public class ClientAppOptions
{
    #region Public Fields
    public const string Section = "ClientApp";
    #endregion Public Fields

    #region Public Properties
    public string SingleTenantHostName { get; set; } = string.Empty;
    public string MultiTenantHostName { get; set; } = string.Empty;
    public string ConfirmEmailChangeUrl { get; set; } = string.Empty;
    public string ConfirmEmailUrl { get; set; } = string.Empty;
    public string ConfirmEmailUrlWithReturnUrl { get; set; } = string.Empty;
    public string ResetPasswordUrl { get; set; } = string.Empty;

    #endregion Public Properties
}
