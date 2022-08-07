using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateOTT.Application.Features.Content.Videos.Queries.GetNewStorageName;
public class GetNewStorageNameQuery : IRequest<Envelope<NewStorageNameResponse>>
{
    #region Public Classes

    public class GetNewStorageNameQueryHandler : IRequestHandler<GetNewStorageNameQuery, Envelope<NewStorageNameResponse>>
    {
        #region Private Fields

        private readonly ITenantUseCase _tenantUseCase;

        public GetNewStorageNameQueryHandler(ITenantUseCase tenantUseCase)
        {
            _tenantUseCase = tenantUseCase;
        }

        #endregion Private Fields

        #region Public Methods

        public async Task<Envelope<NewStorageNameResponse>> Handle(GetNewStorageNameQuery request, CancellationToken cancellationToken)
        {
            var namePrefixResponse = _tenantUseCase.GetTenantStorageNamePrefix();

            if (string.IsNullOrWhiteSpace(namePrefixResponse.StorageFileNamePrefix))
            {
                await _tenantUseCase.AddTenantStorageNamePrefixIfNotExists();
                namePrefixResponse = _tenantUseCase.GetTenantStorageNamePrefix();
            }

            var storageName = $"{namePrefixResponse.StorageFileNamePrefix}/{Guid.NewGuid().ToString().Replace("-", "")}";
            var response = new NewStorageNameResponse { Name = storageName };

            return await Task.FromResult(Envelope<NewStorageNameResponse>.Result.Ok(response));
        }

        #endregion Public Methods
    }

    #endregion Public Classes   
}
