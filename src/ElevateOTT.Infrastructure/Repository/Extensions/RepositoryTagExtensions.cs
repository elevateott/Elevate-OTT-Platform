using ElevateOTT.Domain.Entities.Content;
using ElevateOTT.Infrastructure.Repository.Extensions.Utility;

namespace ElevateOTT.Infrastructure.Repository.Extensions
{
    public static class RepositoryTagExtensions
    {
        // Example of filter extension method
        //public static IQueryable<TagModel> FilterTagModels(this IQueryable<TagModel> tags, uint minAge, uint maxAge) => 
        //    tags.Where(e => (e.Age >= minAge && e.Age <= maxAge));

        public static IQueryable<TagModel> Search(this IQueryable<TagModel> tags, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) 
                return tags; 
            var lowerCaseTerm = searchTerm.Trim().ToLower(); 
            return tags.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<TagModel> Sort(this IQueryable<TagModel> tags, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return tags.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<TagModel>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return tags.OrderBy(e => e.Name);

            return tags.OrderBy(orderQuery);
        }
    }
}
