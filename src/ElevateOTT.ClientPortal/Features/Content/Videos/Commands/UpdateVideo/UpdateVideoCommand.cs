using ElevateOTT.ClientPortal.Models.DTOs;

namespace ElevateOTT.ClientPortal.Features.Content.Videos.Commands.UpdateVideo;

public class UpdateVideoCommand
{
    #region Public Properties

    public Guid Id { get; set; }
    public string? AssetId { get; set; }
    public string? Title { get; set; }
    public string? FileName { get; set; }
    public string? StreamUrl { get; set; }
    public string? ShortDescription { get; set; }
    public string? FullDescription { get; set; }
    public string? PlaybackId { get; set; }
    public AssetCreationStatus StreamCreationStatus { get; set; }
    public PublicationStatus PublicationStatus { get; set; }
    public ContentAccess ContentAccess { get; set; }
    public bool IsTestAsset { get; set; }
    public bool IsHostedOnMux { get; set; }
    public string? BlobName { get; set; }
    public string? LanguageCode { get; set; }
    public TimeSpan? Duration { get; set; }
    public bool AllowDownload { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string? TrailerVideoUrl { get; set; }
    public string? BlobUrl { get; set; }
    public DateTime? UploadedOn { get; set; }
    public DateTime? ReleasedDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public bool Mp4Support { get; set; }
    public string? DownloadUrl { get; set; }
    public string? Passthrough { get; set; }
    public bool ClosedCaptions { get; set; }

    public string SeoTitle { get; set; } = string.Empty;
    public string SeoDescription { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;

    public Guid? TrailerVideoId { get; set; }
    public Guid? FeaturedCategoryVideoId { get; set; }

    public AssetImageDto? PlayerImage { get; set; } = new();
    public AssetImageDto? CatalogImage { get; set; } = new();
    public AssetImageDto? FeaturedCatalogImage { get; set; } = new();
    public AssetImageDto? AnimatedGif { get; set; } = new();

    public bool IsPlayerImageAdded { get; set; }
    public bool IsCatalogImageAdded { get; set; }
    public bool IsFeaturedCatalogImageAdded { get; set; }
    public bool IsAnimatedGifAdded { get; set; }


    public bool HasOneTimePurchasePrice { get; set; }
    public decimal OneTimePurchasePrice { get; set; }
    public bool HasRentalPrice { get; set; }
    public RentalDuration RentalDuration { get; set; }
    public decimal RentalPrice { get; set; }

    public AuthorDto? Author { get; set; }
    public List<AssetImageDto>? VideoImages { get; set; }
    public List<CategoryDto>? Categories { get; set; }

    #endregion Public Properties
}
