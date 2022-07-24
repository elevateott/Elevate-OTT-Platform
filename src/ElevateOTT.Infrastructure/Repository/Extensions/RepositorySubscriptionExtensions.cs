using ElevateOTT.Domain.Entities.Subscriptions;
using ElevateOTT.Infrastructure.Repository.Extensions.Utility;

namespace ElevateOTT.Infrastructure.Repository.Extensions
{
    public static class RepositorySubscriptionExtensions
    {
        public static IQueryable<SubscriptionModel> Search(this IQueryable<SubscriptionModel> subscriptions, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return subscriptions;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return subscriptions.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<SubscriptionModel>? Sort(this IQueryable<SubscriptionModel> subscriptions, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return subscriptions.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<SubscriptionModel>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return subscriptions.OrderBy(e => e.Name);

            return subscriptions.OrderBy(orderQuery);
        }
    }
}
