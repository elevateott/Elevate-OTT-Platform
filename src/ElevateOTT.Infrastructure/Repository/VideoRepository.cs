using ElevateOTT.Domain.Entities.Content;
using ElevateOTT.Infrastructure.Interfaces.Repository;
using ElevateOTT.Infrastructure.Repository.Extensions;

namespace ElevateOTT.Infrastructure.Repository
{
    public class VideoRepository : RepositoryBase<VideoModel>, IVideoRepository
    {
        public VideoRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        //public async Task<PagedList<VideoModel>> GetVideosAsync(Guid tenantId, VideoParameters videoParameters, bool trackChanges)
        //{
        //    var videos = await FindAll(trackChanges)
        //        .Where(a => a.TenantId.Equals(tenantId))
        //        .Search(videoParameters.SearchTerm)
        //        .Sort(videoParameters.OrderBy)
        //        .OrderBy(c => c.Title)
        //        .Skip((videoParameters.PageNumber - 1) * videoParameters.PageSize)
        //        .Take(videoParameters.PageSize)
        //        .ToListAsync();

        //    var count = await FindAll(trackChanges).CountAsync();

        //    return new PagedList<VideoModel>(videos, count, videoParameters.PageNumber, videoParameters.PageSize);
        //}

        public async Task<VideoModel?> GetVideoAsync(Guid videoId, bool trackChanges) =>
            await FindByCondition(v => v.Id.Equals(videoId), trackChanges)
            .SingleOrDefaultAsync();

        public async Task<List<VideoModel>?> FindVideosByConditionAsync(
            Expression<Func<VideoModel, bool>> expression, bool trackChanges) =>
            await FindByCondition(expression, trackChanges).ToListAsync();

        public async Task<VideoModel?> FindVideoByConditionAsync(Expression<Func<VideoModel, bool>> expression, bool trackChanges) =>
            await FindByCondition(expression, trackChanges)
                .SingleOrDefaultAsync();

        //public void CreateVideoForTenant(Guid? tenantId, VideoModel video)
        //{
        //    video.TenantId = tenantId;
        //    Create(video);
        //}

        public async Task<IEnumerable<VideoModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(v => ids.Contains(v.Id), trackChanges).ToListAsync();

        public void DeleteVideo(VideoModel video) => Delete(video);
        public async Task<bool> VideoExistsAsync(Expression<Func<VideoModel, bool>> expression) => await ExistsAsync(expression);
    }
}
