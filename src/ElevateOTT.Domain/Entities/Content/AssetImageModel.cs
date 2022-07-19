using System.ComponentModel.DataAnnotations;

namespace ElevateOTT.Domain.Entities.Content;

[Table("AssetImages")]
public class AssetImageModel : EntityBase
{
    public string Name { get; set; } = string.Empty;

    [Url(ErrorMessage = "Invalid url.")]
    public string Url { get; set; } = string.Empty;

    public int Width { get; set; }

    public int Height { get; set; }

    #region foreign keys
    [ForeignKey(nameof(VideoModel))]
    public Guid? VideoId { get; set; }

    [ForeignKey(nameof(LiveStreamModel))]
    public Guid? LiveStreamId { get; set; }
    #endregion

    #region navigational properties
    public VideoModel? Video { get; set; }
    public LiveStreamModel? LiveStream { get; set; }
    #endregion
}
