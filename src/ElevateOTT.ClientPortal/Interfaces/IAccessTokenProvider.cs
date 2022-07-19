namespace ElevateOTT.ClientPortal.Interfaces;

public interface IAccessTokenProvider
{
    #region Public Methods

    Task<string> TryGetAccessToken();

    #endregion Public Methods
}