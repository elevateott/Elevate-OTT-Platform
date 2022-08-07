using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ElevateOTT.Application.Features.Identity.Tenants.Queries;

public class GetStorageNamePrefixQuery : IRequest<StorageNamePrefixResponse>
{

    #region Public Classes

    public class GetStorageNamePrefixQueryHandler : IRequestHandler<GetStorageNamePrefixQuery, StorageNamePrefixResponse>
    {
        #region Private Fields

        private readonly ITenantUseCase _tenantUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetStorageNamePrefixQueryHandler(ITenantUseCase tenantUseCase)
        {
            _tenantUseCase = tenantUseCase;
        }

        #endregion Public Constructors


        public async Task<StorageNamePrefixResponse> Handle(GetStorageNamePrefixQuery request, CancellationToken cancellationToken)
        {
            var response = _tenantUseCase.GetTenantStorageNamePrefix();
            return await Task.FromResult(response);
        }
    }

    #endregion #region Public Classes
}
