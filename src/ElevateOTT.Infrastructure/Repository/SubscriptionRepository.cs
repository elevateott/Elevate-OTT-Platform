using ElevateOTT.Domain.Entities.Subscriptions;
using ElevateOTT.Infrastructure.Interfaces.Repository;
using ElevateOTT.Infrastructure.Repository.Extensions;

namespace ElevateOTT.Infrastructure.Repository
{
    public class SubscriptionRepository : RepositoryBase<SubscriptionModel>, ISubscriptionRepository
    {
        public SubscriptionRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        //public async Task<PagedList<SubscriptionModel>> GetSubscriptionsAsync(Guid tenantId, SubscriptionParameters subscriptionParameters, bool trackChanges)
        //{
        //    var subscriptions = await FindAll(trackChanges)
        //        .Where(a => a.TenantId.Equals(tenantId))
        //        .Search(subscriptionParameters.SearchTerm)
        //        .Sort(subscriptionParameters.OrderBy)
        //        .OrderBy(c => c.Name)
        //        .Skip((subscriptionParameters.PageNumber - 1) * subscriptionParameters.PageSize)
        //        .Take(subscriptionParameters.PageSize)
        //        .ToListAsync();

        //    var count = await FindAll(trackChanges).CountAsync();

        //    return new PagedList<SubscriptionModel>(subscriptions, count, subscriptionParameters.PageNumber, subscriptionParameters.PageSize);
        //}

        public async Task<SubscriptionModel?> GetSubscriptionAsync(Guid subscriptionId, bool trackChanges) =>
            await FindByCondition(a => a.Id.Equals(subscriptionId), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<SubscriptionModel?> FindSubscriptionByConditionAsync(Expression<Func<SubscriptionModel, bool>> expression, bool trackChanges) =>
            await FindByCondition(expression, trackChanges)
                .SingleOrDefaultAsync();

        //public void CreateSubscriptionForTenant(Guid tenantId, SubscriptionModel subscription)
        //{
        //    subscription.TenantId = tenantId;
        //    Create(subscription);
        //}
        public async Task<IEnumerable<SubscriptionModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();

        public void DeleteSubscription(SubscriptionModel subscription) => Delete(subscription);
        public async Task<bool> SubscriptionExistsAsync(Expression<Func<SubscriptionModel, bool>> expression) => await ExistsAsync(expression);
    }
}
