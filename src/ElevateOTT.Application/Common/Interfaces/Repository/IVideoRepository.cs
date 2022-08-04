using ElevateOTT.Application.Features.Content.Videos.Queries.GetVideos;
using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Application.Common.Interfaces.Repository
{
    public interface IVideoRepository
    {
        Task<VideoModel?> GetVideoAsync(Guid tenantId, Guid authorId, bool trackChanges);
        IQueryable<VideoModel>? GetVideos(Guid tenantId, GetVideosQuery request, bool trackChanges);
        Task<VideoModel?> FindVideoByConditionAsync(Expression<Func<VideoModel, bool>> expression, bool trackChanges);
        void CreateVideoForTenant(Guid tenantId, VideoModel author);
        Task<IEnumerable<VideoModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteVideo(VideoModel author);
        Task<bool> VideoExistsAsync(Expression<Func<VideoModel, bool>> expression);
    }
}
