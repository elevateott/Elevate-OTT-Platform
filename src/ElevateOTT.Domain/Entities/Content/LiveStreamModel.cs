using System.ComponentModel.DataAnnotations;

namespace ElevateOTT.Domain.Entities.Content;

[Table("LiveStreams")]
public class LiveStreamModel : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string Passthrough { get; set; } = string.Empty;

    public string StreamUrl { get; set; } = string.Empty;

    public string StreamKey { get; set; } = string.Empty;

    public StreamType StreamType { get; set; } = StreamType.Hls;

    public string RtmpUrl { get; set; } = string.Empty;

    public string RtmpsUrl { get; set; } = string.Empty;

    public LiveStreamStatus Status { get; set; }

    public float ReconnectWindow { get; set; }

    public string MuxLiveStreamId { get; set; } = string.Empty;

    public bool IsMuxLiveStream { get; set; }

    public DateTime LiveStreamCreatedAt { get; set; }

    public LatencyMode LatencyMode { get; set; }

    public int MaxContinuousDuration { get; set; }

    [StringLength(300)]
    public string Title { get; set; } = string.Empty;

    public string Sku { get; set; } = string.Empty;

    public DateTime StartDateTime { get; set; }

    [StringLength(2000)]
    public string FullDescription { get; set; } = string.Empty;

    [StringLength(500)]
    public string ShortDescription { get; set; } = string.Empty;

    [StringLength(300)]
    public string? PreRegistrationText { get; set; } = string.Empty;

    public VideoResolutionType VideoResolutionType { get; set; }

    public string Language { get; set; } = string.Empty;

    public string Rating { get; set; } = string.Empty;

    public bool IsClosedCaption { get; set; }      


    [StringLength(30)]
    public string ButtonPurchaseText { get; set; } = string.Empty;


    [StringLength(6, MinimumLength = 2, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
    public string TestLiveStreamPasscode { get; set; } = string.Empty;

    #region foreign keys
    //[ForeignKey(nameof(TenantModel))]
    //public Guid? TenantId { get; set; }
    #endregion

    #region navigational properties
    //public TenantModel? Tenant { get; set; }

    // public List<CommentModel>? Comments { get; set; }
    // public List<MuxPlaybackIdModel>? MuxPlaybackIds { get; set; }
    public List<SubtitleModel>? Subtitles { get; set; }
    public List<AssetImageModel>? LiveStreamImages { get; set; }
    //public SeoMetaDataModel? SeoMetaData { get; set; }
    #endregion
}
