using Ardalis.GuardClauses;
using ElevateOTT.Application.Common.Extensions;
using ElevateOTT.Application.Common.Interfaces.Repository;
using ElevateOTT.Application.Features.Content.Categories.Queries.GetCategories;
using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Infrastructure.Repository
{
    public sealed class CategoryRepository : RepositoryBase<CategoryModel>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
        {
        }

        public IQueryable<CategoryModel>? GetCategories(Guid tenantId, GetCategoriesQuery? request, bool trackChanges)
        {
            Guard.Against.Null(request, nameof(request));

            var query = FindAll(trackChanges)
                .Where(a => a.TenantId.Equals(tenantId));

            if (!string.IsNullOrWhiteSpace(request.SearchText))
                query = query.Where(r => r.Title.Contains(request.SearchText));

            query = !string.IsNullOrWhiteSpace(request.SortBy)
                ? query.SortBy(request.SortBy)
                : query.OrderBy(a => a.Title);

            return query;
        }

        public async Task<CategoryModel?> GetCategoryAsync(Guid tenantId, Guid categoryId, bool trackChanges) =>
            await FindByCondition(a => a.TenantId.Equals(tenantId)
                                       && a.Id.Equals(categoryId), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<CategoryModel?> FindCategoryByConditionAsync(Expression<Func<CategoryModel, bool>> expression, bool trackChanges) =>
            await FindByCondition(expression, trackChanges)
                .SingleOrDefaultAsync();

        public void CreateCategory(CategoryModel category)
        {
            Create(category);
        }

        public Task<IEnumerable<CategoryModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void DeleteCategory(CategoryModel category) => Delete(category);

        public async Task<bool> CategoryExistsAsync(Expression<Func<CategoryModel, bool>> expression) => await ExistsAsync(expression);
    }
}
