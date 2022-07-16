﻿namespace BinaryPlate.Application.Common.Interfaces.Services.StorageServices;

public interface IStorageFactory
{
    #region Public Methods

    IFileStorageService CreateInstance(StorageTypes storageTypes);

    #endregion Public Methods
}