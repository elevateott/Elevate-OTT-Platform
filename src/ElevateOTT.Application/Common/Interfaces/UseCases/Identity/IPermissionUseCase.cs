﻿namespace ElevateOTT.Application.Common.Interfaces.UseCases.Identity;

public interface IPermissionUseCase
{
    #region Public Methods

    Task<Envelope<PermissionsResponse>> GetLoadedOnDemandPermissions(GetPermissionsQuery request);

    Task<Envelope<PermissionsResponse>> GetLoadedOneShotPermissions();

    #endregion Public Methods
}