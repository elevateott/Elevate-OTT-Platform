using ElevateOTT.Application.Features.Identity.Tenants.Queries;

namespace ElevateOTT.Application.Common.Interfaces.UseCases.Identity;

public interface ITenantUseCase
{
    #region Public Methods

    Task<Envelope<CreateTenantResponse>> AddTenant(CreateTenantCommand request);
    StorageNamePrefixResponse GetTenantStorageNamePrefix();

    #endregion Public Methods
}
