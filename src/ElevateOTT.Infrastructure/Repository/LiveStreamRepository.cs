using ElevateOTT.Application.Common.Interfaces.Repository;
using ElevateOTT.Domain.Entities.Content;
using ElevateOTT.Infrastructure.Repository.Extensions;

namespace ElevateOTT.Infrastructure.Repository
{
    public class LiveStreamRepository : RepositoryBase<LiveStreamModel>, ILiveStreamRepository
    {
        public LiveStreamRepository(RepositoryContext applicationDbContext)
            : base(applicationDbContext)
        {
        }

        //public async Task<PagedList<LiveStreamModel>> GetLiveStreamsAsync(Guid tenantId, LiveStreamParameters liveStreamParameters, bool trackChanges)
        //{
        //    var liveStreams = await EntityFrameworkQueryableExtensions.ToListAsync(FindAll(trackChanges)
        //            .Where(a => a.TenantId.Equals(tenantId))
        //            .Search(liveStreamParameters.SearchTerm));

        //    // TODO
        //    // .Search(liveStreamParameters.SearchTerm).ToListAsync();
        //    //.Sort(liveStreamParameters.OrderBy)
        //    //.OrderBy(c => c.Name)
        //    //.Skip((liveStreamParameters.PageNumber - 1) * liveStreamParameters.PageSize)
        //    //.Take(liveStreamParameters.PageSize)
        //    //.ToListAsync();

        //    var count = await FindAll(trackChanges).CountAsync();

        //    return new PagedList<LiveStreamModel>(liveStreams, count, liveStreamParameters.PageNumber, liveStreamParameters.PageSize);

        //    throw new NotImplementedException();

        //}

        public async Task<LiveStreamModel?> GetLiveStreamAsync(Guid liveStreamId, bool trackChanges) =>
            await FindByCondition(a => a.Id.Equals(liveStreamId), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<LiveStreamModel?> FindLiveStreamByConditionAsync(Expression<Func<LiveStreamModel, bool>> expression, bool trackChanges) =>
            await FindByCondition(expression, trackChanges)
                .SingleOrDefaultAsync();

        public async Task<LiveStreamModel?> GetLiveStreamByPassthroughAsync(string passthrough, bool trackChanges) =>
            await FindByCondition(u => u.Passthrough.Equals(passthrough), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<LiveStreamModel?> GetLiveStreamByMuxLiveStreamIdAsync(string muxLiveStreamId, bool trackChanges) =>
            await FindByCondition(u => u.MuxLiveStreamId.Equals(muxLiveStreamId), trackChanges)
                .SingleOrDefaultAsync();

        //public void CreateLiveStreamForTenant(Guid tenantId, LiveStreamModel liveStream)
        //{
        //    liveStream.TenantId = tenantId;
        //    Create(liveStream);
        //}

        public async Task<IEnumerable<LiveStreamModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();

        public void DeleteLiveStream(LiveStreamModel liveStream) => Delete(liveStream);
        public async Task<bool> LiveStreamExistsAsync(Expression<Func<LiveStreamModel, bool>> expression) => await ExistsAsync(expression);
    }
}
