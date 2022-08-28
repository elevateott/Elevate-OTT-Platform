using ElevateOTT.Domain.Common.DTOs;
using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Application.Features.Content.Videos.Queries.GetVideos;

public class VideoItem : BaseAssetDto
{
    #region Public Properties

    public Guid Id { get; set; }
    public bool Mp4Support { get; set; }
    public Guid? TrailerVideoId { get; set; }
    public Guid? FeaturedCategoryVideoId { get; set; }  
    public Guid? AuthorId { get; set; }

    public bool HasOneTimePurchasePrice { get; set; }
    public decimal OneTimePurchasePrice { get; set; }
    public bool HasRentalPrice { get; set; }
    public RentalDuration RentalDuration { get; set; }
    public decimal RentalPrice { get; set; }

    public AuthorDto? Author { get; set; }
    public List<AssetImageDto>? VideoImages { get; set; }
    public List<TagDto>? Tags { get; set; }

    #endregion Public Properties


}
