using ElevateOTT.Domain.Entities.Content;
using ElevateOTT.Infrastructure.Repository.Extensions.Utility;

namespace ElevateOTT.Infrastructure.Repository.Extensions
{
    public static class RepositoryVideoExtensions
    {
        // Example of filter extension method
        //public static IQueryable<VideoModel> FilterVideoModels(this IQueryable<VideoModel> videos, uint minAge, uint maxAge) => 
        //    videos.Where(e => (e.Age >= minAge && e.Age <= maxAge));

        public static IQueryable<VideoModel> Search(this IQueryable<VideoModel> videos, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return videos;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return videos.Where(e => e.Title.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<VideoModel> Sort(this IQueryable<VideoModel> videos, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return videos.OrderBy(e => e.Title);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<VideoModel>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return videos.OrderBy(e => e.Title);

            return videos.OrderBy(orderQuery);
        }
    }
}
