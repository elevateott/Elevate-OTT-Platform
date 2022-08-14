using ElevateOTT.Application.Common.Interfaces.Repository;
using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Infrastructure.Repository
{
    public sealed class CollectionRepository : RepositoryBase<CollectionModel>, ICollectionRepository
    {
        public CollectionRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
        {
        }

        //public async Task<PagedList<CollectionModel>> GetCollectionsAsync(Guid tenantId, CollectionParameters collectionParameters, bool trackChanges)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<CollectionModel?> GetCollectionAsync(Guid collectionId, bool trackChanges) =>
            await FindByCondition(a => a.Id.Equals(collectionId), trackChanges)
            .SingleOrDefaultAsync();

        public async Task<CollectionModel?> FindCollectionByConditionAsync(Expression<Func<CollectionModel, bool>> expression, bool trackChanges) =>
            await FindByCondition(expression, trackChanges)
                .SingleOrDefaultAsync();

        //public void CreateCollectionForTenant(Guid tenantId, CollectionModel collection)
        //{
        //    collection.TenantId = tenantId;
        //    Create(collection);
        //}
        public async Task<IEnumerable<CollectionModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();

        public void DeleteCollection(CollectionModel collection) => Delete(collection);
        public async Task<bool> CollectionExistsAsync(Expression<Func<CollectionModel, bool>> expression) => await ExistsAsync(expression);
    }
}
