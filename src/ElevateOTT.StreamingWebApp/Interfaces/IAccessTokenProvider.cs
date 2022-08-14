namespace ElevateOTT.StreamingWebApp.Interfaces;

public interface IAccessTokenProvider
{
    #region Public Methods

    Task<string> TryGetAccessToken();

    #endregion Public Methods
}