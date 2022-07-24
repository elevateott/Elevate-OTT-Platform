using ElevateOTT.Application.Common.Interfaces.Repository;
using ElevateOTT.Domain.Entities.Content;
using ElevateOTT.Infrastructure.Repository.Extensions;

namespace ElevateOTT.Infrastructure.Repository
{
    public sealed class CategoryRepository : RepositoryBase<CategoryModel>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext applicationDbContext)
        : base(applicationDbContext)
        {
        }

        //public async Task<PagedList<CategoryModel>> GetCategoriesAsync(Guid tenantId, CategoryParameters categoryParameters, bool trackChanges)
        //{
        //    var categories = await Queryable.Skip(FindAll(trackChanges)
        //            .Where(a => a.TenantId.Equals(tenantId))
        //            .Search(categoryParameters.SearchTerm ?? string.Empty)
        //            .Sort(categoryParameters.OrderBy ?? string.Empty), (categoryParameters.PageNumber - 1) * categoryParameters.PageSize)
        //        .Take(categoryParameters.PageSize)
        //        .ToListAsync();

        //    var count = await FindAll(trackChanges).CountAsync();

        //    return new PagedList<CategoryModel>(categories, count, categoryParameters.PageNumber, categoryParameters.PageSize);
        //}

        public async Task<CategoryModel?> GetCategoryAsync(Guid categoryId, bool trackChanges) =>
            await FindByCondition(a => a.Id.Equals(categoryId), trackChanges)
            .SingleOrDefaultAsync();

        public async Task<CategoryModel?> FindCategoryByConditionAsync(Expression<Func<CategoryModel, bool>> expression, bool trackChanges) =>
            await FindByCondition(expression, trackChanges)
                .SingleOrDefaultAsync();

        //public void CreateCategoryForTenant(Guid tenantId, CategoryModel category)
        //{
        //    category.TenantId = tenantId;
        //    Create(category);
        //}
        public async Task<IEnumerable<CategoryModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();

        public void DeleteCategory(CategoryModel category) => Delete(category);
        public async Task<bool> CategoryExistsAsync(Expression<Func<CategoryModel, bool>> expression) => await ExistsAsync(expression);
    }
}
