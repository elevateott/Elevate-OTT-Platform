using ElevateOTT.Domain.Entities.Content;
using ElevateOTT.Infrastructure.Repository.Extensions.Utility;

namespace ElevateOTT.Infrastructure.Repository.Extensions
{
    public static class RepositoryGenreExtensions
    {
        // Example of filter extension method
        //public static IQueryable<GenreModel> FilterGenreModels(this IQueryable<GenreModel> genres, uint minAge, uint maxAge) => 
        //    genres.Where(e => (e.Age >= minAge && e.Age <= maxAge));

        public static IQueryable<GenreModel> Search(this IQueryable<GenreModel> genres, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) 
                return genres; 
            var lowerCaseTerm = searchTerm.Trim().ToLower(); 
            return genres.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<GenreModel> Sort(this IQueryable<GenreModel> genres, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return genres.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<GenreModel>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return genres.OrderBy(e => e.Name);

            return genres.OrderBy(orderQuery);
        }
    }
}
