namespace ElevateOTT.ClientPortal.Models;

public class AuthResponse
{
    #region Public Properties

    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;

    #endregion Public Properties
}
