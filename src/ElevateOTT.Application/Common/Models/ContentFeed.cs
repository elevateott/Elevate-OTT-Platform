using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Wordprocessing;
using ElevateOTT.Application.Common.Models.Chargebee;

namespace ElevateOTT.Application.Common.Models;

public class ContentFeed
{
    //ref: https://developer.roku.com/docs/specs/direct-publisher-feed-specs/json-dp-spec.md#category


    public ContentFeed()
    {
        Categories = new List<CategoryItem>();
        Playlists = new List<PlaylistItem>();
        Movies = new List<MovieItem>();
        ShortFormVideos = new List<ShortFormVideoItem>();
        Series = new List<SeriesItem>();
        LiveFeeds = new List<LiveFeedItem>();
    }


    [JsonPropertyName("providerName")]
    public string ProviderName { get; set; } = string.Empty;

    [JsonPropertyName("language")]
    public string Language { get; set; } = "en-US";

    [JsonPropertyName("lastUpdated")]
    public string LastUpdated { get; set; } = string.Empty;

    [JsonPropertyName("version")]
    public string Version { get; set; } = string.Empty;

    [JsonPropertyName("categories")]
    public List<CategoryItem> Categories { get; set; }

    [JsonPropertyName("playlists")]
    public List<PlaylistItem> Playlists { get; set; }

    [JsonPropertyName("movies")]
    public List<MovieItem> Movies { get; set; }

    [JsonPropertyName("shortFormVideos")]
    public List<ShortFormVideoItem> ShortFormVideos { get; set; }

    [JsonPropertyName("series")]
    public List<SeriesItem> Series { get; set; }

    [JsonPropertyName("liveFeeds")]
    public List<LiveFeedItem> LiveFeeds { get; set; }



    public class CategoryItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("playlistName")]
        public string PlaylistName { get; set; } = string.Empty;

        [JsonPropertyName("query")]
        public string Query { get; set; } = string.Empty;

        [JsonPropertyName("order")]
        public string Order { get; set; } = "manual";
    }

    public class PlaylistItem
    {
        public PlaylistItem()
        {
            ItemIds = new List<string>();
        }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("itemIds")] 
        public List<string> ItemIds { get; set; }
    }

    public class MovieItem
    {
        public MovieItem()
        {
            Genres = new List<string>();
            Tags = new List<string>();
            Content = new ContentItem();
            Rating = new RatingItem();
            Credits = new List<CreditItem>();
        }

        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("shortDescription")]
        public string ShortDescription { get; set; } = string.Empty;

        [JsonPropertyName("longDescription")]
        public string LongDescription { get; set; } = string.Empty;

        [JsonPropertyName("thumbnail")]
        public string Thumbnail { get; set; } = string.Empty;

        [JsonPropertyName("releaseDate")]
        public string ReleaseDate { get; set; } = string.Empty;

        [JsonPropertyName("genres")]
        public List<string> Genres { get; set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }

        [JsonPropertyName("content")]
        public ContentItem Content { get; set; }

        [JsonPropertyName("rating")]
        public RatingItem Rating { get; set; }

        [JsonPropertyName("credits")]
        public List<CreditItem> Credits { get; set; }

        [JsonPropertyName("premiumContent")]
        public bool PremiumContent { get; set; }
    }

    public class ShortFormVideoItem
    {

    }

    public class SeriesItem
    {

    }

    public class LiveFeedItem
    {
        public LiveFeedItem()
        {
            Genres = new List<string>();
            Tags = new List<string>();
            Content = new ContentItem();
            Rating = new RatingItem();
        }

        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public ContentItem Content { get; set; } 

        [JsonPropertyName("thumbnail")]
        public string Thumbnail { get; set; } = string.Empty;

        [JsonPropertyName("brandedThumbnail")]
        public string BrandedThumbnail { get; set; } = string.Empty;

        [JsonPropertyName("shortDescription")]
        public string ShortDescription { get; set; } = string.Empty;

        [JsonPropertyName("longDescription")]
        public string LongDescription { get; set; } = string.Empty;

        [JsonPropertyName("releaseDateTime")]
        public string ReleaseDateTime { get; set; } = string.Empty;

        [JsonPropertyName("rating")]
        public RatingItem Rating { get; set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }

        [JsonPropertyName("genres")]
        public List<string> Genres { get; set; }
    }

    public class RatingItem
    {
        [JsonPropertyName("rating")]
        public string RatingProperty { get; set; } = string.Empty;

        [JsonPropertyName("ratingSource")]
        public string RatingSource { get; set; } = string.Empty;
    }

    public class ContentItem
    {
        public ContentItem()
        {
            Videos = new List<VideoItem>();
            Captions = new List<CaptionItem>();
        }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; } = string.Empty;

        [JsonPropertyName("dateAdded")]
        public string DateAdded { get; set; } = string.Empty;

        // trickPlayFiles
        // validityPeriodStart
        // validityPeriodEnd
        // adBreaks

        [JsonPropertyName("videos")]
        public List<VideoItem> Videos { get; set; }

        [JsonPropertyName("captions")]
        public List<CaptionItem> Captions { get; set; }
    }

    public class VideoItem
    {
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("quality")]
        public string Quality { get; set; } = string.Empty;

        [JsonPropertyName("videoType")]
        public string VideoType { get; set; } = string.Empty;
    }

    public class CaptionItem
    {

    }

    public class CreditItem
    {

    }

}


