using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Application.Common.Interfaces.Repository
{
    public interface ICategoryRepository
    {
        //Task<PagedList<CategoryModel>> GetCategoriesAsync(Guid tenantId, CategoryParameters categoryParameters, bool trackChanges);
        Task<CategoryModel?> GetCategoryAsync(Guid categoryId, bool trackChanges);
        Task<CategoryModel?> FindCategoryByConditionAsync(Expression<Func<CategoryModel, bool>> expression, bool trackChanges);
        //void CreateCategoryForTenant(Guid tenantId, CategoryModel category);
        Task<IEnumerable<CategoryModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteCategory(CategoryModel category);
        Task<bool> CategoryExistsAsync(Expression<Func<CategoryModel, bool>> expression);
    }
}
