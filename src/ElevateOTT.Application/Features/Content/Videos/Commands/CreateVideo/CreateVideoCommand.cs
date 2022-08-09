using ElevateOTT.Application.Common.Interfaces.UseCases.Content;

namespace ElevateOTT.Application.Features.Content.Videos.Commands.CreateVideo;

public class CreateVideoCommand : IRequest<Envelope<CreateVideoResponse>>
{
    #region Public Properties

    public Guid TenantId { get; set; }
    public string? AssetId { get; set; }
    public string? Title { get; set; }
    public string? FileName { get; set; }
    public string? StreamUrl { get; set; }
    public string? ShortDescription { get; set; }
    public string? FullDescription { get; set; }
    public string? PlaybackId { get; set; }
    public AssetCreationStatus StreamCreationStatus { get; set; }
    public PublicationStatus PublicationStatus { get; set; }
    public ContentAccess ContentAccess { get; set; }
    public bool IsTestAsset { get; set; }
    public bool IsHostedOnMux { get; set; }
    public string? BlobName { get; set; }
    public string? LanguageCode { get; set; }
    public TimeSpan? Duration { get; set; }
    public bool AllowDownload { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string? TrailerVideoUrl { get; set; }
    public string? BlobUrl { get; set; }
    public DateTime? UploadedOn { get; set; }
    public DateTime? ReleasedDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public bool Mp4Support { get; set; }
    public string? DownloadUrl { get; set; }
    public string? Passthrough { get; set; }
    public bool ClosedCaptions { get; set; }

    // public Guid? VideoFolderId { get; set; }

    // TODO can videoUseCase be in multiple collections??
    //
    // public Guid? CollectionId { get; set; }

    #endregion Public Properties



    #region Public Classes
    public class CreateVideoCommandHandler : IRequestHandler<CreateVideoCommand, Envelope<CreateVideoResponse>>
    {
        #region Private Fields
        private readonly IVideoUseCase _videoUseCaseUseCase;
        #endregion Private Fields

        #region Public Constructors
        public CreateVideoCommandHandler(IVideoUseCase videoUseCase)
        {
            _videoUseCaseUseCase = videoUseCase;
        }
        #endregion Public Constructors

        #region Public Methods
        public async Task<Envelope<CreateVideoResponse>> Handle(CreateVideoCommand request, CancellationToken cancellationToken)
        {
            return await _videoUseCaseUseCase.AddVideo(request);
        }
        #endregion Public Methods
    }
    #endregion Public Classes
}
