using ElevateOTT.Domain.Entities.Products;
using ElevateOTT.Infrastructure.Repository.Extensions.Utility;

namespace ElevateOTT.Infrastructure.Repository.Extensions
{
    public static class RepositoryItemPriceExtensions
    {
        public static IQueryable<ItemPriceModel> Search(this IQueryable<ItemPriceModel> itemPrices, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return itemPrices;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return itemPrices.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<ItemPriceModel>? Sort(this IQueryable<ItemPriceModel> itemPrices, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return itemPrices.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<ItemPriceModel>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return itemPrices.OrderBy(e => e.Name);

            return itemPrices.OrderBy(orderQuery);
        }
    }
}
