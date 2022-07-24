using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Application.Common.Interfaces.Repository
{
    public interface ITagRepository
    {
        //Task<PagedList<TagModel>> GetTagsAsync(Guid tenantId, TagParameters tagParameters, bool trackChanges);
        Task<TagModel?> GetTagAsync(Guid tagId, bool trackChanges);
        Task<TagModel?> FindTagByConditionAsync(Expression<Func<TagModel, bool>> expression, bool trackChanges);
        //void CreateTagForTenant(Guid tenantId, TagModel tag);
        Task<IEnumerable<TagModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteTag(TagModel tag);
        Task<bool> TagExistsAsync(Expression<Func<TagModel, bool>> expression);
    }
}
