using ElevateOTT.ClientPortal.Models.DTOs;

namespace ElevateOTT.ClientPortal.Features.Content.Videos.Commands.UpdateVideo;

public class UpdateVideoCommand
{
    #region Public Properties

    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? FileName { get; set; }
    public string? ShortDescription { get; set; }
    public string? FullDescription { get; set; }
    public PublicationStatus PublicationStatus { get; set; }
    public ContentAccess ContentAccess { get; set; }
    public string? LanguageCode { get; set; }
    public TimeSpan? Duration { get; set; }
    public bool AllowDownload { get; set; }
    public DateTime? ReleasedDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public bool Mp4Support { get; set; }
    public string? Passthrough { get; set; }
    public bool ClosedCaptions { get; set; }
    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }
    public string? Slug { get; set; }

    public bool HasOneTimePurchasePrice { get; set; }
    public decimal OneTimePurchasePrice { get; set; }
    public bool HasRentalPrice { get; set; }
    public RentalDuration RentalDuration { get; set; }
    public decimal RentalPrice { get; set; }

    public Guid? TrailerVideoId { get; set; }
    public Guid? FeaturedCategoryVideoId { get; set; }

    public IFormFile? PlayerImage { get; set; }
    public IFormFile? CatalogImage { get; set; }
    public IFormFile? FeaturedCatalogImage { get; set; }
    public IFormFile? AnimatedGif { get; set; }

    public ImageState PlayerImageState { get; set; }
    public ImageState CatalogImageState { get; set; }
    public ImageState FeaturedCatalogImageState { get; set; }
    public ImageState AnimatedGifState { get; set; }

    public string? ThumbnailUrl { get; set; }
    public string? PlayerImageUrl { get; set; }
    public string? CatalogImageUrl { get; set; }
    public string? FeaturedCatalogImageUrl { get; set; }
    public string? AnimatedGifUrl { get; set; }

    public Guid? AuthorId { get; set; }

    public List<Guid>? CategoryIds { get; set; }

    #endregion Public Properties
}
