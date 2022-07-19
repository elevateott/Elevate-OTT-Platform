using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Infrastructure.Interfaces.Repository
{
    public interface ISeoMetaDataRepository
    {
        //Task<PagedList<SeoMetaDataModel>> GetSeoMetaDatasAsync(Guid tenantId, SeoMetaDataParameters seoMetaDataParameters, bool trackChanges);
        Task<SeoMetaDataModel?> GetSeoMetaDataAsync(Guid seoMetaDataId, bool trackChanges);
        Task<SeoMetaDataModel?> FindSeoMetaDataByConditionAsync(Expression<Func<SeoMetaDataModel, bool>> expression, bool trackChanges);
        void CreateSeoMetaDataForTenant(Guid tenantId, SeoMetaDataModel seoMetaData);
        Task<IEnumerable<SeoMetaDataModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteSeoMetaData(SeoMetaDataModel seoMetaData);
        Task<bool> SeoMetaDataExistsAsync(Expression<Func<SeoMetaDataModel, bool>> expression);
    }
}
