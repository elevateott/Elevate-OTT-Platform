using ElevateOTT.Domain.Entities.Products;
using ElevateOTT.Infrastructure.Interfaces.Repository;
using ElevateOTT.Infrastructure.Repository.Extensions;

namespace ElevateOTT.Infrastructure.Repository
{
    public class ItemPriceRepository : RepositoryBase<ItemPriceModel>, IItemPriceRepository
    {
        public ItemPriceRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        //public async Task<PagedList<ItemPriceModel>> GetItemPricesAsync(Guid productItemId, ItemPriceParameters itemPriceParameters, bool trackChanges)
        //{
        //    var itemPrices = await Queryable.Skip(FindAll(trackChanges)
        //            .Where(a => a.ProductItemId.Equals(productItemId))
        //            .Search(itemPriceParameters.SearchTerm ?? string.Empty)
        //            .Sort(itemPriceParameters.OrderBy ?? string.Empty), (itemPriceParameters.PageNumber - 1) * itemPriceParameters.PageSize)
        //        .Take(itemPriceParameters.PageSize)
        //        .ToListAsync();

        //    var count = await FindAll(trackChanges).CountAsync();

        //    return new PagedList<ItemPriceModel>(itemPrices, count, itemPriceParameters.PageNumber, itemPriceParameters.PageSize);
        //}

        public async Task<ItemPriceModel?> GetItemPriceAsync(Guid itemPriceId, bool trackChanges) =>
            await FindByCondition(a => a.Id.Equals(itemPriceId), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<ItemPriceModel?> FindItemPriceByConditionAsync(Expression<Func<ItemPriceModel, bool>> expression, bool trackChanges) =>
            await FindByCondition(expression, trackChanges)
                .SingleOrDefaultAsync();

        public void CreateItemPriceForProductItem(Guid productItemId, ItemPriceModel itemPrice)
        {
            itemPrice.ProductItemId = productItemId;
            Create(itemPrice);
        }
        public async Task<IEnumerable<ItemPriceModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();

        public void DeleteItemPrice(ItemPriceModel itemPrice) => Delete(itemPrice);
        public async Task<bool> ItemPriceExistsAsync(Expression<Func<ItemPriceModel, bool>> expression) => await ExistsAsync(expression);
    }
}
