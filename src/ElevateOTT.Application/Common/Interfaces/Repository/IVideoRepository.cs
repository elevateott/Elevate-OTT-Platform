using ElevateOTT.Application.Features.Content.Videos.Queries.GetVideos;
using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Application.Common.Interfaces.Repository
{
    public interface IVideoRepository
    {
        Task<VideoModel?> GetVideoAsync(Guid videoId, bool trackChanges);
        VideoModel? GetVideoByPassthrough(string passthrough);
        IQueryable<VideoModel>? GetVideos(GetVideosQuery request, bool trackChanges);
        Task<VideoModel?> FindVideoByConditionAsync(Expression<Func<VideoModel, bool>> expression, bool trackChanges);
        void CreateVideoForTenant(VideoModel video);
        Task<IEnumerable<VideoModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteVideo(VideoModel video);
        Task<bool> VideoExistsAsync(Expression<Func<VideoModel, bool>> expression);
    }
}
