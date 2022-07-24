using System.ComponentModel.DataAnnotations;

namespace ElevateOTT.Domain.Entities.Content;

public abstract class BaseAsset : BaseEntity
{
    [Url(ErrorMessage = "Invalid url.")]
    public string StreamUrl { get; set; } = string.Empty;

    #region mux-specific properties
    public string PlaybackId { get; set; } = string.Empty;
    public string AssetId { get; set; } = string.Empty;
    public AssetCreationStatus StreamCreationStatus { get; set; }
    #endregion

    public PublicationStatus PublicationStatus { get; set; }

    public bool IsTestAsset { get; set; } 

    public bool IsHostedOnMux { get; set; } 

    public string FileName { get; set; } = string.Empty;

    public string BlobName { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string ShortDescription { get; set; } = string.Empty;

    // TODO HTML content??
    public string FullDescription { get; set; } = string.Empty;

    public string LanguageCode { get; set; } = string.Empty;

    public TimeSpan? Duration { get; set; }

    public bool AllowDownload { get; set; }

    [Url(ErrorMessage = "Invalid url.")]
    public string ThumbnailUrl { get; set; } = string.Empty;

    [Url(ErrorMessage = "Invalid url.")]
    public string TrailerVideoUrl { get; set; } = string.Empty;

    [Url(ErrorMessage = "Invalid url.")]
    public string BlobUrl { get; set; } = string.Empty;

    public ContentAccess ContentAccess { get; set; }

    public DateTime? UploadedOn { get; set; }

    public DateTime? ReleasedDate { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public Guid? LanguageCodeId { get; set; }
}
