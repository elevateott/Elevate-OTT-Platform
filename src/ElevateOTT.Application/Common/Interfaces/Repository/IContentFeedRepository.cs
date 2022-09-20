using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Application.Common.Interfaces.Repository
{
    public interface IContentFeedRepository
    {
        IQueryable<ContentFeedModel> GetContentFeeds(bool trackChanges);
        Task<ContentFeedModel?> GetContentFeedAsync(Guid contentFeedId, bool trackChanges);
        Task<ContentFeedModel?> FindContentFeedByConditionAsync(Expression<Func<ContentFeedModel, bool>> expression, bool trackChanges);
        void CreateContentFeedForTenant(ContentFeedModel contentFeed);
        Task<IEnumerable<ContentFeedModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteContentFeed(ContentFeedModel contentFeed);
        Task<bool> ContentFeedExistsAsync(Expression<Func<ContentFeedModel, bool>> expression);
    }
}
