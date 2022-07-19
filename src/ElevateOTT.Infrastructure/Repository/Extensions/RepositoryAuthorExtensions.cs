using ElevateOTT.Domain.Entities.Content;
using ElevateOTT.Infrastructure.Repository.Extensions.Utility;

namespace ElevateOTT.Infrastructure.Repository.Extensions
{
    public static class RepositoryAuthorExtensions
    {
        // Example of filter extension method
        //public static IQueryable<AuthorModel> FilterAuthorModels(this IQueryable<AuthorModel> authors, uint minAge, uint maxAge) => 
        //    authors.Where(e => (e.Age >= minAge && e.Age <= maxAge));

        public static IQueryable<AuthorModel> Search(this IQueryable<AuthorModel> authors, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) 
                return authors; 
            var lowerCaseTerm = searchTerm.Trim().ToLower(); 
            return authors.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<AuthorModel> Sort(this IQueryable<AuthorModel> authors, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return authors.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<AuthorModel>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return authors.OrderBy(e => e.Name);

            return authors.OrderBy(orderQuery);
        }
    }
}
