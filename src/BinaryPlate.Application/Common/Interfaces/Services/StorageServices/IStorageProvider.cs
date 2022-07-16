namespace BinaryPlate.Application.Common.Interfaces.Services.StorageServices;

public interface IStorageProvider
{
    #region Public Methods

    Task<IFileStorageService> InvokeInstanceAsync();

    #endregion Public Methods
}