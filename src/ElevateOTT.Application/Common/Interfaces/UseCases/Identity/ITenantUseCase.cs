using ElevateOTT.Application.Features.Identity.Tenants.Queries;

namespace ElevateOTT.Application.Common.Interfaces.UseCases.Identity;

public interface ITenantUseCase
{
    #region Public Methods

    Tenant? GetTenant();
    Task<Envelope<CreateTenantResponse>> AddTenant(CreateTenantCommand request);
    StorageNamePrefixResponse GetTenantStorageNamePrefix();
    Task AddTenantStorageNamePrefixIfNotExists();

    #endregion Public Methods
}
