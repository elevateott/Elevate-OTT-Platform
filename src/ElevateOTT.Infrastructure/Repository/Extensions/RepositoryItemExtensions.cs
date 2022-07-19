using ElevateOTT.Domain.Entities.Products;
using ElevateOTT.Infrastructure.Repository.Extensions.Utility;

namespace ElevateOTT.Infrastructure.Repository.Extensions
{
    public static class RepositoryItemExtensions
    {
        public static IQueryable<ProductItemModel> Search(this IQueryable<ProductItemModel> items, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return items;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return items.Where(e => e.Title.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<ProductItemModel> Sort(this IQueryable<ProductItemModel> items, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return items.OrderBy(e => e.Title);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<ProductItemModel>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return items.OrderBy(e => e.Title);

            return items.OrderBy(orderQuery);
        }
    }
}
