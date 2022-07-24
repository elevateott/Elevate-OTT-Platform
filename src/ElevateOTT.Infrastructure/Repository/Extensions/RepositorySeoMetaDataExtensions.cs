using ElevateOTT.Domain.Entities.Content;
using ElevateOTT.Infrastructure.Repository.Extensions.Utility;

namespace ElevateOTT.Infrastructure.Repository.Extensions
{
    public static class RepositorySeoMetaDataExtensions
    {
        // Example of filter extension method
        //public static IQueryable<SeoMetaDataModel> FilterSeoMetaDataModels(this IQueryable<SeoMetaDataModel> authors, uint minAge, uint maxAge) => 
        //    authors.Where(e => (e.Age >= minAge && e.Age <= maxAge));

        public static IQueryable<SeoMetaDataModel> Search(this IQueryable<SeoMetaDataModel> authors, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) 
                return authors; 
            var lowerCaseTerm = searchTerm.Trim().ToLower(); 
            return authors.Where(e => e.SeoTitle.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<SeoMetaDataModel>? Sort(this IQueryable<SeoMetaDataModel> authors, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return authors.OrderBy(e => e.SeoTitle);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<SeoMetaDataModel>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return authors.OrderBy(e => e.SeoTitle);

            return authors.OrderBy(orderQuery);
        }
    }
}
