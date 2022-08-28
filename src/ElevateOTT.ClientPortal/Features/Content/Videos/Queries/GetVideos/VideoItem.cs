using System.ComponentModel.DataAnnotations;
using ElevateOTT.ClientPortal.Models.DTOs;

namespace ElevateOTT.ClientPortal.Features.Content.Videos.Queries.GetVideos;

public class VideoItem : BaseAssetDto
{
    #region Public Properties

    public Guid Id { get; set; }    
    public bool Mp4Support { get; set; }    

    public Guid? TrailerVideoId { get; set; }
    public Guid? FeaturedCategoryVideoId { get; set; }

    public AuthorDto? Author { get; set; }
    public List<AssetImageDto>? VideoImages { get; set; }
    public List<CategoryDto>? Categories { get; set; }
    public List<TagDto>? Tags { get; set; }

    #endregion Public Properties
}
