using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Application.Common.Interfaces.Repository
{
    public interface IVideoRepository
    {
        //Task<PagedList<VideoModel>> GetVideosAsync(Guid tenantId, VideoParameters videoParameters, bool trackChanges);

        Task<VideoModel?> GetVideoAsync(Guid videoId, bool trackChanges);

        Task<List<VideoModel>?> FindVideosByConditionAsync(Expression<Func<VideoModel, bool>> expression, bool trackChanges);

        Task<VideoModel?> FindVideoByConditionAsync(Expression<Func<VideoModel, bool>> expression, bool trackChanges);

        //void CreateVideoForTenant(Guid? tenantId, VideoModel video);

        Task<IEnumerable<VideoModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);

        void DeleteVideo(VideoModel video);

        Task<bool> VideoExistsAsync(Expression<Func<VideoModel, bool>> expression);
    }
}
