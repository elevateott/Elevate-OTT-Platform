using Ardalis.GuardClauses;
using ElevateOTT.Application.Common.Interfaces.Repository;
using ElevateOTT.Application.Features.Content.Videos.Queries.GetVideos;
using ElevateOTT.Domain.Entities.Content;
using ElevateOTT.Infrastructure.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ElevateOTT.Infrastructure.Repository
{
    public class VideoRepository : RepositoryBase<VideoModel>, IVideoRepository
    {
        public VideoRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
        {
        }

        public IQueryable<VideoModel>? GetVideos(Guid tenantId, GetVideosQuery? request, bool trackChanges)
        {
            Guard.Against.Null(request, nameof(request));

            var query = FindAll(trackChanges)
                .Include(v => v.Author)
                .Where(v => v.TenantId.Equals(tenantId));

            if (!string.IsNullOrWhiteSpace(request.SearchText))
                query = query
                    .Include(v => v.Author)
                    .Where(v => v.Title != null && v.Title.Contains(request.SearchText)
                || v.FileName != null && v.FileName.Contains(request.SearchText));

            query = !string.IsNullOrWhiteSpace(request.SortBy)
                ? query.SortBy(request.SortBy)
                : query.OrderBy(v => v.Title).ThenBy(v => v.FileName);

            return query;
        }

        public VideoModel? GetVideoByPassthrough(string passthrough)
        {
            var video = ApplicationDbContext?.Videos?.FirstOrDefault(v => v.Passthrough != null && v.Passthrough.Equals(passthrough));
            return video;
        }

        public async Task<VideoModel?> GetVideoAsync(Guid tenantId, Guid videoId, bool trackChanges) =>
            await FindByCondition(a => a.TenantId.Equals(tenantId)
                                       && a.Id.Equals(videoId), trackChanges)
                .Include(v => v.Author)
                .Include(v => v.VideosCategories)
                .SingleOrDefaultAsync();

        public async Task<VideoModel?> FindVideoByConditionAsync(Expression<Func<VideoModel, bool>> expression, bool trackChanges) =>
            await FindByCondition(expression, trackChanges)
                .Include(v => v.Author)
                .Include(v => v.VideosCategories)
                .SingleOrDefaultAsync();

        public void CreateVideoForTenant(Guid tenantId, VideoModel video)
        {
            video.TenantId = tenantId;
            video.CreatedOn = DateTime.Now;
            Create(video);
        }

        public Task<IEnumerable<VideoModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void DeleteVideo(VideoModel video) => Delete(video);

        public async Task<bool> VideoExistsAsync(Expression<Func<VideoModel, bool>> expression) => await ExistsAsync(expression);
    }
}
