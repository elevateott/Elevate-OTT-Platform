﻿namespace BinaryPlate.BlazorPlate.Interfaces.Consumers;

public interface IPermissionsClient
{
    #region Public Methods

    Task<HttpResponseWrapper<object>> GetPermissions(GetPermissionsQuery request);

    #endregion Public Methods
}