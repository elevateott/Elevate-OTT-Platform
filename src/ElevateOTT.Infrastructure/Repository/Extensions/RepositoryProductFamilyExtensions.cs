using ElevateOTT.Domain.Entities.Products;
using ElevateOTT.Infrastructure.Repository.Extensions.Utility;

namespace ElevateOTT.Infrastructure.Repository.Extensions
{
    public static class RepositoryProductFamilyExtensions
    {
        public static IQueryable<ProductFamilyModel> Search(this IQueryable<ProductFamilyModel> productFamilies, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return productFamilies;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return productFamilies.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<ProductFamilyModel> Sort(this IQueryable<ProductFamilyModel> productFamilies, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return productFamilies.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<ProductFamilyModel>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return productFamilies.OrderBy(e => e.Name);

            return productFamilies.OrderBy(orderQuery);
        }
    }
}
