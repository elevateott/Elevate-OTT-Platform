namespace ElevateOTT.BlazorPlate.Interfaces;

public interface IAccessTokenProvider
{
    #region Public Methods

    Task<string> TryGetAccessToken();

    #endregion Public Methods
}