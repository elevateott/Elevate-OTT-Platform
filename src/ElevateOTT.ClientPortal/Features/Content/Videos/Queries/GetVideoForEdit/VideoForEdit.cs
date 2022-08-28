using System.ComponentModel;
using ElevateOTT.ClientPortal.Annotations;
using System.Runtime.CompilerServices;
using ElevateOTT.ClientPortal.Models.DTOs;
using ElevateOTT.ClientPortal.Features.Content.Categories.Queries.GetCategoriesForAutoComplete;
using ElevateOTT.ClientPortal.Features.Content.Videos.Queries.GetVideosForAutoComplete;

namespace ElevateOTT.ClientPortal.Features.Content.Videos.Queries.GetVideoForEdit;

public class VideoForEdit : BaseAssetDto, INotifyPropertyChanged
{
    #region Public Properties

    public event PropertyChangedEventHandler? PropertyChanged;

    private string? _title = string.Empty;

    public override string? Title
    {
        get
        {
            return _title;
        }

        set
        {
            if (value != _title)
            {
                _title = value;
                OnPropertyChanged();
            }
        }
    }

    public Guid Id { get; set; }    
    public bool Mp4Support { get; set; }
    public bool HasOneTimePurchasePrice { get; set; }
    public decimal OneTimePurchasePrice { get; set; }
    public bool HasRentalPrice { get; set; }
    public RentalDuration RentalDuration { get; set; }
    public decimal RentalPrice { get; set; }

    public Guid? TrailerVideoId { get; set; }
    public Guid? FeaturedCategoryVideoId { get; set; }

    public VideoItemForAutoComplete? TrailerVideo { get; set; }
    public VideoItemForAutoComplete? FeaturedCategoryVideo { get; set; }

    public AssetImageDto? PlayerImage { get; set; } = new();
    public AssetImageDto? CatalogImage { get; set; } = new();
    public AssetImageDto? FeaturedCatalogImage { get; set; } = new ();
    public AssetImageDto? AnimatedGif { get; set; } = new();

    public ImageState PlayerImageState { get; set; }
    public ImageState CatalogImageState { get; set; }
    public ImageState FeaturedCatalogImageState { get; set; }
    public ImageState AnimatedGifState { get; set; }

    public Guid? AuthorId { get; set; }
    public AuthorDto? Author { get; set; }
    public List<AssetImageDto>? VideoImages { get; set; }
    public List<Guid>? CategoryIds { get; set; }
    public List<TagDto>? Tags { get; set; }

    #endregion Public Properties


    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
