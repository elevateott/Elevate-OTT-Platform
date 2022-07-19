using ElevateOTT.Domain.Entities.Content;
using ElevateOTT.Infrastructure.Repository.Extensions.Utility;

namespace ElevateOTT.Infrastructure.Repository.Extensions
{
    public static class RepositoryLiveStreamExtensions
    {
        // Example of filter extension method
        //public static IQueryable<LiveStreamModel> FilterLiveStreamModels(this IQueryable<LiveStreamModel> liveStreams, uint minAge, uint maxAge) => 
        //    liveStreams.Where(e => (e.Age >= minAge && e.Age <= maxAge));

        public static IQueryable<LiveStreamModel> Search(this IQueryable<LiveStreamModel> liveStreams, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return liveStreams;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return liveStreams.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<LiveStreamModel> Sort(this IQueryable<LiveStreamModel> liveStreams, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return liveStreams.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<LiveStreamModel>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return liveStreams.OrderBy(e => e.Name);

            return liveStreams.OrderBy(orderQuery);
        }
    }
}
