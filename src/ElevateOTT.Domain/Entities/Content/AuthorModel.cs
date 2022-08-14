﻿using System.ComponentModel.DataAnnotations;

namespace ElevateOTT.Domain.Entities.Content;

[Table("Authors")]
public class AuthorModel : BaseEntity, IMustHaveTenant
{
    public Guid TenantId { get; set; }

    [Required(ErrorMessage = "Author name is a required field.")]
    [MaxLength(50, ErrorMessage = "Maximum length for the Name is 50 characters.")]
    public string? Name { get; set; } 

    [MaxLength(2000, ErrorMessage = "Maximum length for the Bio is 2000 characters.")]
    public string? Bio { get; set; } 

    [Url(ErrorMessage = "Image URL must be a valid URL.")]
    public string? ImageUrl { get; set; }

    [MaxLength(250, ErrorMessage = "Maximum length for the SEO Title is 250 characters.")]
    public string? SeoTitle { get; set; } 

    [MaxLength(1000, ErrorMessage = "Maximum length for the SEO Description is 1000 characters.")]
    public string? SeoDescription { get; set; } 

    // TODO validation for no spaces and no special chars
    public string? Slug { get; set; }


    #region Navigational Properties

    public List<VideoModel>? Videos { get; set; }
    public List<LiveStreamModel>? LiveStreams { get; set; }
    public List<PodcastModel>? Podcasts { get; set; }

    #endregion
}
