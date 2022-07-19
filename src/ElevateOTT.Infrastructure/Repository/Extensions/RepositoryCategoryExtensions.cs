using ElevateOTT.Domain.Entities.Content;
using ElevateOTT.Infrastructure.Repository.Extensions.Utility;

namespace ElevateOTT.Infrastructure.Repository.Extensions
{
    public static class RepositoryCategoryExtensions
    {
        // Example of filter extension method
        //public static IQueryable<CategoryModel> FilterCategoryModels(this IQueryable<CategoryModel> categorys, uint minAge, uint maxAge) => 
        //    categorys.Where(e => (e.Age >= minAge && e.Age <= maxAge));

        public static IQueryable<CategoryModel> Search(this IQueryable<CategoryModel> categorys, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) 
                return categorys; 
            var lowerCaseTerm = searchTerm.Trim().ToLower(); 
            return categorys.Where(e => e.Title.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<CategoryModel> Sort(this IQueryable<CategoryModel> categorys, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return categorys.OrderBy(e => e.Title);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<CategoryModel>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return categorys.OrderBy(e => e.Title);

            return categorys.OrderBy(orderQuery);
        }
    }
}
