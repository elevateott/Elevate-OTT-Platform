using System.ComponentModel.DataAnnotations;
using ElevateOTT.Domain.Entities.Mux;

namespace ElevateOTT.Domain.Entities.Content;

[Table("Videos")]
public class VideoModel : BaseAsset, IMustHaveTenant
{
    // TODO filters
    // TODO Geo-blocking

    // TODO move asset base props in here

    public Guid TenantId { get; set; }

    public bool Mp4Support { get; set; }

    [Url(ErrorMessage = "Invalid url.")]
    public string? DownloadUrl { get; set; } 
    public string? Passthrough { get; set; } 
    public bool ClosedCaptions { get; set; }

    #region foreign keys

    //[ForeignKey(nameof(VideoFolderModel))]
    //public Guid? VideoFolderId { get; set; }

    //[ForeignKey(nameof(CollectionModel))]
    //public Guid? CollectionId { get; set; }
    #endregion

    #region navigational properties
    //public CollectionModel? Collection { get; set; }
    //public VideoFolderModel? VideoFolder { get; set; }
    //public ICollection<VideoAuthorModel>? VideosAuthors { get; set; }
    //public ICollection<VideoCategoryModel>? VideosCategories { get; set; }
    //public List<ExtraModel>? Extras { get; set; }
    //public List<CommentModel>? Comments { get; set; }
    //public List<MuxPlaybackIdModel>? MuxPlaybackIds { get; set; }
    //public List<SubtitleModel>? Subtitles { get; set; }
    //public List<AssetImageModel>? VideoImages { get; set; }
    //public SeoMetaDataModel? SeoMetaData { get; set; }
    #endregion
}
