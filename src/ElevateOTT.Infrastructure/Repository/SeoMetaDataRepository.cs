using ElevateOTT.Domain.Entities.Content;
using ElevateOTT.Infrastructure.Interfaces.Repository;

namespace ElevateOTT.Infrastructure.Repository
{
    public sealed class SeoMetaDataRepository : RepositoryBase<SeoMetaDataModel>, ISeoMetaDataRepository
    {
        public SeoMetaDataRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }


        //public Task<PagedList<SeoMetaDataModel>> GetSeoMetaDatasAsync(Guid tenantId, SeoMetaDataParameters seoMetaDataParameters, bool trackChanges)
        //{
        //    throw new NotImplementedException();
        //}

        public Task<SeoMetaDataModel?> GetSeoMetaDataAsync(Guid seoMetaDataId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<SeoMetaDataModel?> FindSeoMetaDataByConditionAsync(Expression<Func<SeoMetaDataModel, bool>> expression, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void CreateSeoMetaDataForTenant(Guid tenantId, SeoMetaDataModel seoMetaData)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SeoMetaDataModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void DeleteSeoMetaData(SeoMetaDataModel seoMetaData)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SeoMetaDataExistsAsync(Expression<Func<SeoMetaDataModel, bool>> expression) => await ExistsAsync(expression);
    }
}
