using Ardalis.GuardClauses;
using Azure.Core;
using ElevateOTT.Application.Common.Interfaces.Repository;
using ElevateOTT.Domain.Entities;
using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Infrastructure.Repository
{
    public sealed class ContentFeedRepository : RepositoryBase<ContentFeedModel>, IContentFeedRepository
    {
        public ContentFeedRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
        {
        }
        public IQueryable<ContentFeedModel> GetContentFeeds(bool trackChanges)
        {
            // TODO may not need tenant id
            var query = FindAll(trackChanges);

            return query;
        }

        public async Task<ContentFeedModel?> GetContentFeedAsync(Guid contentFeedId, bool trackChanges) =>
            await FindByCondition(a => a.Id.Equals(contentFeedId), trackChanges)
            .SingleOrDefaultAsync();

        public async Task<ContentFeedModel?> FindContentFeedByConditionAsync(Expression<Func<ContentFeedModel, bool>> expression, bool trackChanges) =>
            await FindByCondition(expression, trackChanges)
                .SingleOrDefaultAsync();

        public void CreateContentFeedForTenant(ContentFeedModel contentFeed)
        {
            Create(contentFeed);
        }

        public async Task<IEnumerable<ContentFeedModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();

        public void DeleteContentFeed(ContentFeedModel contentFeed) => Delete(contentFeed);
        public async Task<bool> ContentFeedExistsAsync(Expression<Func<ContentFeedModel, bool>> expression) => await ExistsAsync(expression);
    }
}
