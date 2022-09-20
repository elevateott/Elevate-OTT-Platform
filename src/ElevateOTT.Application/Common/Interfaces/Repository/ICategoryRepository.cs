using ElevateOTT.Application.Features.Content.Categories.Queries.GetCategories;
using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Application.Common.Interfaces.Repository
{
    public interface ICategoryRepository
    {
        Task<CategoryModel?> GetCategoryAsync(Guid categoryId, bool trackChanges);
        IQueryable<CategoryModel>? GetCategories(GetCategoriesQuery request, bool trackChanges);
        Task<CategoryModel?> FindCategoryByConditionAsync(Expression<Func<CategoryModel, bool>> expression, bool trackChanges);
        void CreateCategory(CategoryModel category);
        Task<IEnumerable<CategoryModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteCategory(CategoryModel category);
        Task<bool> CategoryExistsAsync(Expression<Func<CategoryModel, bool>> expression);
    }
}
