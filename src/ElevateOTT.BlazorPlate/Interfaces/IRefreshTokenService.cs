namespace ElevateOTT.BlazorPlate.Interfaces;

public interface IRefreshTokenService
{
    #region Public Methods

    Task<string> TryRefreshToken();

    #endregion Public Methods
}