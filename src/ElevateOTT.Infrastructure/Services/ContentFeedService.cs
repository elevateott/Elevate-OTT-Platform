using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using ElevateOTT.Application.Common.Interfaces.Repository;
using ElevateOTT.Application.Features.Content.Categories.Queries.GetCategories;
using ElevateOTT.Application.Features.Content.ContentFeeds.Queries.GetContentFeed;
using ElevateOTT.Application.Features.Content.Videos.Queries.GetVideos;
using ElevateOTT.Domain.Entities.Content;
using Microsoft.EntityFrameworkCore;

namespace ElevateOTT.Infrastructure.Services;

public class ContentFeedService : IContentFeedService
{
    #region Private Fields

    private readonly ITenantResolver _tenantResolver;
    private readonly IConfigReaderService _configReaderService;
    private readonly IFileStorageService _blobStorageService;

    private readonly JsonSerializerOptions _options =
        new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };


    #endregion

    #region Public Constructors

    public ContentFeedService(ITenantResolver tenantResolver, 
        IFileStorageService blobStorageService, 
        IConfigReaderService configReaderService)
    {
        _tenantResolver = tenantResolver;
        _blobStorageService = blobStorageService;
        _configReaderService = configReaderService;
    }

    #endregion

    #region Public Methods

    public async Task CreateContentFeed(IApplicationDbContext dbContext)
    {
        // TODO language code set on client in settings
        // only one language content feed per subscription, but language should be set by user
        // en-US language code by default

        if (dbContext?.Tenants != null)
        {
            Tenant? tenant = await dbContext.Tenants.FirstOrDefaultAsync(t => t.Id.Equals(_tenantResolver.GetTenantId()));
            Guard.Against.Null(tenant, nameof(tenant));
            Guard.Against.Null(tenant.StorageFileNamePrefix, nameof(tenant.StorageFileNamePrefix));
            string storageFileNamePrefix = tenant.StorageFileNamePrefix;
            string channelName = tenant.ChannelName ?? string.Empty;

            var blobOptions = _configReaderService.GetBlobOptions();
            int currentContentFeedVersion = int.Parse(blobOptions.ContentFeedVersion);

            // Accounting for multiple content feed versions 

            var contentFeedsQuery = dbContext.ContentFeeds?.AsQueryable();

            // No content feeds have been created in the db or none have current version
            if (contentFeedsQuery != null && (!contentFeedsQuery.Any() || contentFeedsQuery.All(c => c.Version != currentContentFeedVersion)))
            {
                // TODO language code should come from settings

                var contentFeedEntity = new ContentFeedModel
                {
                    ProviderName = channelName,
                    Version = currentContentFeedVersion,
                    LanguageCode = tenant?.LanguageCode ?? "en-US",
                };

                dbContext.ContentFeeds?.Add(contentFeedEntity);
                await dbContext.SaveChangesAsync();
            }

            // TODO make sure one for tenant
            if (contentFeedsQuery != null)
            {
                var contentFeedEntities = await contentFeedsQuery.ToListAsync();

                // Store content feeds as json files in blob storage then update db
                foreach (var contentFeedEntity in contentFeedEntities)
                {
                    var contentFeed = await GenerateContentFeed(contentFeedEntity, dbContext);
                    string jsonStr = SerializeJson(contentFeed);
                    string fileUrl = await _blobStorageService.SaveContentFeed(jsonStr, storageFileNamePrefix, contentFeedEntity.Version);
                    contentFeedEntity.Url = fileUrl;
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }


    private string SerializeJson(object obj)
    {
        var options = new JsonSerializerOptions(_options)
        {
            WriteIndented = true
        };

        return JsonSerializer.Serialize(obj, options);
    }

    private async Task<ContentFeed> GenerateContentFeed(ContentFeedModel contentFeedEntity, IApplicationDbContext dbContext)
    {
        Guard.Against.Null(contentFeedEntity, nameof(contentFeedEntity));
        //Guard.Against.Null(dbContext, nameof(dbContext));

        var contentFeed = new ContentFeed
        {
            ProviderName = contentFeedEntity.ProviderName ?? string.Empty,
            Language = contentFeedEntity.LanguageCode ?? "en-US",
            LastUpdated = GetISO8601DateTimeString(),
            Version = contentFeedEntity.Version.ToString()
        };

        // TODO categories
        // playlist

        //var categoryEntities = await dbContext.Categories.Include("VideosCategories").ToListAsync();

        //var categoriesQuery = _repositoryManager.Category.GetCategories(new GetCategoriesQuery(), true);
        if (dbContext?.Categories != null)
        {
            var categoriesQuery = dbContext.Categories.Include(nameof(CategoryModel.VideosCategories)).AsQueryable();
            if (categoriesQuery.Any())
            {
                var categories = await categoriesQuery.ToListAsync();

                foreach (var category in categories)
                {
                    var videoIds = category.VideosCategories.Select(vc => vc.VideoId.ToString());

                    // Create playlist for category
                    string playlistName = $"{category.Title?.ToLower()} content";
                    contentFeed.Playlists.Add(new ContentFeed.PlaylistItem
                    {
                        Name = playlistName,
                        ItemIds = videoIds.ToList()
                    });

                    contentFeed.Categories.Add(new ContentFeed.CategoryItem
                    {
                        Name = category.Title ?? string.Empty,
                        PlaylistName = playlistName,
                    });
                }
            }
        }


        //var videos = _repositoryManager.Video.GetVideos(new GetVideosQuery(), false);
        if (dbContext?.Videos == null)
        {
            return contentFeed;
        }

        var videos = await dbContext.Videos.Include(nameof(VideoModel.VideosCategories)).ToListAsync();

        foreach (var video in videos)
        {
            if (!video.VideosCategories.Any()) continue;
            if (video.PublicationStatus != PublicationStatus.Published) continue;

            contentFeed.Movies.Add(new ContentFeed.MovieItem
            {
                Id = video.Id.ToString(),
                Title = video.Title ?? string.Empty,
                ShortDescription = video.ShortDescription ?? string.Empty,
                LongDescription = video.FullDescription ?? string.Empty,
                Thumbnail = video.ThumbnailUrl ?? string.Empty,
                ReleaseDate = GetISO8601DateTimeString(video.ReleasedDate),
                PremiumContent = video.ContentAccess != ContentAccess.Free,
                Content = new ContentFeed.ContentItem
                {
                    Duration = 100,  // video.Duration.
                    Language = "en-US",
                    DateAdded = GetISO8601DateTimeString(video.CreatedOn),
                    Videos = new List<ContentFeed.VideoItem>
                    {
                        new ContentFeed.VideoItem
                        {
                            Url = video.ContentAccess == ContentAccess.Free ? (video.PublicStreamUrl ?? string.Empty) : (video.SignedStreamUrl ?? string.Empty),
                            Quality = "FHD",
                            VideoType = "HLS"
                        }
                    }
                },
                // Genres
                // Tags
                // Rating
                // Credits
            });
        }


        // TODO
        // live feeds
        // podcasts
        // author


        return contentFeed;
    }

    private string GetISO8601DateTimeString()
    {
        return DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture);
    }

    private string GetISO8601DateTimeString(DateTime? dt)
    {
        return dt.HasValue ? dt.Value.ToUniversalTime().ToString("s", System.Globalization.CultureInfo.InvariantCulture) : string.Empty;
    }

    #endregion
}
