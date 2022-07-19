using System.ComponentModel.DataAnnotations;

namespace ElevateOTT.Domain.Entities.Content;

public abstract class AssetBase : EntityBase
{
    [Url(ErrorMessage = "Invalid url.")]
    public string? StreamUrl { get; set; }

    #region mux-specific properties
    public string? PlaybackId { get; set; }
    public string? AssetId { get; set; }
    public AssetCreationStatus StreamCreationStatus { get; set; }
    #endregion

    public PublicationStatus PublicationStatus { get; set; }

    public bool IsTestAsset { get; set; }

    public bool IsHostedOnMux { get; set; }

    public string? FileName { get; set; }

    public string? BlobName { get; set; }

    public string? Title { get; set; }

    public string? ShortDescription { get; set; }

    // TODO HTML content??
    public string? FullDescription { get; set; }

    public string? LanguageCode { get; set; }

    public TimeSpan? Duration { get; set; }

    public bool AllowDownload { get; set; }

    [Url(ErrorMessage = "Invalid url.")]
    public string? ThumbnailUrl { get; set; }

    [Url(ErrorMessage = "Invalid url.")]
    public string? TrailerVideoUrl { get; set; }

    [Url(ErrorMessage = "Invalid url.")]
    public string? BlobUrl { get; set; }

    public ContentAccess ContentAccess { get; set; }

    public DateTime? UploadedOn { get; set; }

    public DateTime? ReleasedDate { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public Guid? LanguageCodeId { get; set; }
}
