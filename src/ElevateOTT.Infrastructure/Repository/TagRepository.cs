using ElevateOTT.Domain.Entities.Content;
using ElevateOTT.Infrastructure.Interfaces.Repository;
using ElevateOTT.Infrastructure.Repository.Extensions;

namespace ElevateOTT.Infrastructure.Repository
{
    public sealed class TagRepository : RepositoryBase<TagModel>, ITagRepository
    {
        public TagRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        //public async Task<PagedList<TagModel>> GetTagsAsync(Guid tenantId, TagParameters tagParameters, bool trackChanges)
        //{
        //    var tags = await FindAll(trackChanges)
        //        .Where(a => a.TenantId.Equals(tenantId))
        //        .Search(tagParameters.SearchTerm)
        //        .Sort(tagParameters.OrderBy)
        //        .OrderBy(c => c.Name)
        //        .Skip((tagParameters.PageNumber - 1) * tagParameters.PageSize)
        //        .Take(tagParameters.PageSize)
        //        .ToListAsync();

        //    var count = await FindAll(trackChanges).CountAsync();

        //    return new PagedList<TagModel>(tags, count, tagParameters.PageNumber, tagParameters.PageSize);
        //}

        public async Task<TagModel?> GetTagAsync(Guid tagId, bool trackChanges) =>
            await FindByCondition(a => a.Id.Equals(tagId), trackChanges)
            .SingleOrDefaultAsync();

        public async Task<TagModel?> FindTagByConditionAsync(Expression<Func<TagModel, bool>> expression, bool trackChanges) =>
            await FindByCondition(expression, trackChanges)
                .SingleOrDefaultAsync();

        //public void CreateTagForTenant(Guid tenantId, TagModel tag)
        //{
        //    tag.TenantId = tenantId;
        //    Create(tag);
        //}
        public async Task<IEnumerable<TagModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();

        public void DeleteTag(TagModel tag) => Delete(tag);
        public async Task<bool> TagExistsAsync(Expression<Func<TagModel, bool>> expression) => await ExistsAsync(expression);
    }
}
