using System.ComponentModel.DataAnnotations;
using ElevateOTT.Domain.Entities.Mux;

namespace ElevateOTT.Domain.Entities.Content;

[Table("Videos")]
public class VideoModel : BaseAsset, IMustHaveTenant
{
    // TODO filters
    // TODO Geo-blocking

    public Guid TenantId { get; set; }

    public bool Mp4Support { get; set; }


    #region foreign keys

    [ForeignKey(nameof(AuthorModel))]
    public Guid? AuthorId { get; set; }

    #endregion

    #region Navigational Properties
    public ICollection<VideoCategoryModel>? VideosCategories { get; set; }
    public ICollection<VideoCollectionModel>? VideosCollections { get; set; }
    public AuthorModel? Author { get; set; }
    public List<AssetImageModel>? VideoImages { get; set; }

    //public List<ExtraModel>? Extras { get; set; }
    //public List<CommentModel>? Comments { get; set; }
    //public List<SubtitleModel>? Subtitles { get; set; }
    #endregion
}
