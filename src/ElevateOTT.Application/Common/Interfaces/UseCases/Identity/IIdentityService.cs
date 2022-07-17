namespace ElevateOTT.Application.Common.Interfaces.UseCases.Identity;

public interface IIdentityService
{
    #region Public Methods

    Task<string> GetUserNameAsync(string userId);

    #endregion Public Methods
}