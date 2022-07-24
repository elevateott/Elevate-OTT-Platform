namespace ElevateOTT.Domain.Entities.Content;

public class SubtitleModel : BaseEntity
{
    public bool ClosedCaption { get; set; }

    public string TrackId { get; set; } = string.Empty;

    public string Passthrough { get; set; } = string.Empty;

    public Guid? LanguageCodeId { get; set; }

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
