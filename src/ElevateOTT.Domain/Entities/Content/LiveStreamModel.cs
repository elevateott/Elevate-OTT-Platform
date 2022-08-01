using System.ComponentModel.DataAnnotations;

namespace ElevateOTT.Domain.Entities.Content;

[Table("LiveStreams")]
public class LiveStreamModel : BaseEntity
{
    public string? Name { get; set; } 

    public string? Passthrough { get; set; } 

    public string? StreamUrl { get; set; } 

    public string? StreamKey { get; set; } 

    public StreamType StreamType { get; set; } = StreamType.Hls;

    public string? RtmpUrl { get; set; } 

    public string? RtmpsUrl { get; set; } 

    public LiveStreamStatus Status { get; set; }

    public float ReconnectWindow { get; set; }

    public string? MuxLiveStreamId { get; set; } 

    public bool IsMuxLiveStream { get; set; }

    public DateTime LiveStreamCreatedAt { get; set; }

    public LatencyMode LatencyMode { get; set; }

    public int MaxContinuousDuration { get; set; }

    [StringLength(300)]
    public string? Title { get; set; } 

    public string? Sku { get; set; } 

    public DateTime StartDateTime { get; set; }

    [StringLength(2000)]
    public string? FullDescription { get; set; } 

    [StringLength(500)]
    public string? ShortDescription { get; set; } 

    [StringLength(300)]
    public string? PreRegistrationText { get; set; } 

    public VideoResolutionType VideoResolutionType { get; set; }

    public string? Language { get; set; } 

    public string? Rating { get; set; } 

    public bool IsClosedCaption { get; set; }      


    [StringLength(30)]
    public string? ButtonPurchaseText { get; set; } 


    [StringLength(6, MinimumLength = 2, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
    public string? TestLiveStreamPasscode { get; set; } 

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
