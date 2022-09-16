﻿using ElevateOTT.Application.Features.Content.Categories.Queries.GetCategoriesForAutoComplete;
using ElevateOTT.Application.Features.Content.Videos.Queries.GetVideosForAutoComplete;
using ElevateOTT.Domain.Common.DTOs;

namespace ElevateOTT.Application.Features.Content.Videos.Queries.GetVideoForEdit;

public class VideoForEdit : BaseAssetDto
{
    #region Public Properties
    public Guid Id { get; set; }    
    public bool Mp4Support { get; set; } 

    public bool HasOneTimePurchasePrice { get; set; }
    public double OneTimePurchasePrice { get; set; }
    public bool HasRentalPrice { get; set; }
    public RentalDuration RentalDuration { get; set; }
    public double RentalPrice { get; set; }

    public Guid? TrailerVideoId { get; set; }
    public Guid? FeaturedCategoryVideoId { get; set; }

    public VideoItemForAutoComplete? TrailerVideo { get; set; }
    public VideoItemForAutoComplete? FeaturedCategoryVideo { get; set; }

    public AssetImageDto? PlayerImage { get; set; }
    public AssetImageDto? CatalogImage { get; set; }
    public AssetImageDto? FeaturedCatalogImage { get; set; }
    public AssetImageDto? AnimatedGif { get; set; }

    public ImageState PlayerImageState { get; set; }
    public ImageState CatalogImageState { get; set; }
    public ImageState FeaturedCatalogImageState { get; set; }
    public ImageState AnimatedGifState { get; set; }

    public Guid? AuthorId { get; set; }
    public AuthorDto? Author { get; set; }

    public List<AssetImageDto>? VideoImages { get; set; }
    public List<TagDto>? Tags { get; set; }

    public List<Guid>? CategoryIds { get; set; }

    #endregion Public Properties
}
