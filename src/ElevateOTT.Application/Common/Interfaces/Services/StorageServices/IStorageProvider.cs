namespace ElevateOTT.Application.Common.Interfaces.Services.StorageServices;

public interface IStorageProvider
{
    #region Public Methods
    Task<IFileStorageService> InvokeInstanceAsync();
    IFileStorageService InvokeInstanceForAzureStorage();

    #endregion Public Methods
}
