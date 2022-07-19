using ElevateOTT.Domain.Entities.Products;

namespace ElevateOTT.Infrastructure.Interfaces.Repository
{
    public interface IProductItemRepository
    {
        //Task<PagedList<ProductItemModel>> GetItemsAsync(Guid productFamilyId, ItemParameters itemParameters, bool trackChanges);
        Task<ProductItemModel?> GetItemAsync(Guid itemId, bool trackChanges);
        Task<ProductItemModel?> FindItemByConditionAsync(Expression<Func<ProductItemModel, bool>> expression, bool trackChanges);
        void CreateProductItemForProductFamily(Guid productFamilyId, ProductItemModel item);
        Task<IEnumerable<ProductItemModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteItem(ProductItemModel item);
        Task<bool> ProductItemExistsAsync(Expression<Func<ProductItemModel, bool>> expression);
    }
}
