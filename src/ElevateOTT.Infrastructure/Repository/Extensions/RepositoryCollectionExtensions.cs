using ElevateOTT.Domain.Entities.Content;
using ElevateOTT.Infrastructure.Repository.Extensions.Utility;

namespace ElevateOTT.Infrastructure.Repository.Extensions
{
    public static class RepositoryCollectionExtensions
    {
        // Example of filter extension method
        //public static IQueryable<CollectionModel> FilterCollectionModels(this IQueryable<CollectionModel> collections, uint minAge, uint maxAge) => 
        //    collections.Where(e => (e.Age >= minAge && e.Age <= maxAge));

        public static IQueryable<CollectionModel> Search(this IQueryable<CollectionModel> collections, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) 
                return collections; 
            var lowerCaseTerm = searchTerm.Trim().ToLower(); 
            return collections.Where(e => e.Title.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<CollectionModel>? Sort(this IQueryable<CollectionModel> collections, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return collections.OrderBy(e => e.Title);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<CollectionModel>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return collections.OrderBy(e => e.Title);

            return collections.OrderBy(orderQuery);
        }
    }
}
