using ElevateOTT.Domain.Entities.Products;
using ElevateOTT.Infrastructure.Interfaces.Repository;
using ElevateOTT.Infrastructure.Repository.Extensions;

namespace ElevateOTT.Infrastructure.Repository
{
    public class ProductItemRepository : RepositoryBase<ProductItemModel>, IProductItemRepository
    {
        public ProductItemRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        //public async Task<PagedList<ProductItemModel>> GetItemsAsync(Guid productFamilyId, ItemParameters itemParameters, bool trackChanges)
        //{
        //    var items = await Queryable.Skip(FindAll(trackChanges)
        //            .Where(a => a.ProductFamilyId.Equals(productFamilyId))
        //            .Search(itemParameters.SearchTerm ?? string.Empty)
        //            .Sort(itemParameters.OrderBy ?? string.Empty), (itemParameters.PageNumber - 1) * itemParameters.PageSize)
        //        .Take(itemParameters.PageSize)
        //        .ToListAsync();

        //    var count = await FindAll(trackChanges).CountAsync();

        //    return new PagedList<ProductItemModel>(items, count, itemParameters.PageNumber, itemParameters.PageSize);
        //}

        public async Task<ProductItemModel?> GetItemAsync(Guid itemId, bool trackChanges) =>
            await FindByCondition(a => a.Id.Equals(itemId), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<ProductItemModel?> FindItemByConditionAsync(Expression<Func<ProductItemModel, bool>> expression, bool trackChanges) =>
            await FindByCondition(expression, trackChanges)
                .SingleOrDefaultAsync();

        public void CreateProductItemForProductFamily(Guid productFamilyId, ProductItemModel item)
        {
            item.ProductFamilyId = productFamilyId;
            Create(item);
        }
        public async Task<IEnumerable<ProductItemModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();

        public void DeleteItem(ProductItemModel item) => Delete(item);
        public async Task<bool> ProductItemExistsAsync(Expression<Func<ProductItemModel, bool>> expression) => await ExistsAsync(expression);
    }
}
