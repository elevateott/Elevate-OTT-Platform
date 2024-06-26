﻿namespace ElevateOTT.StreamingWebApp.Interfaces.Consumers;

public interface ITenantsClient
{
    #region Public Methods

    Task<HttpResponseWrapper<object>> CreateTenant(CreateTenantCommand request);

    #endregion Public Methods
}