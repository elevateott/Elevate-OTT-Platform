using ElevateOTT.Application.Common.Interfaces.UseCases.Content;

namespace ElevateOTT.Application.Features.Content.Videos.Commands.UpdateVideo;

public class UpdateVideoCommand : IRequest<Envelope<string>>
{

    #region Public Properties

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string SeoTitle { get; set; } = string.Empty;
    public string SeoDescription { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public IFormFile? ImageFile { get; set; }
    public bool IsImageAdded { get; set; }

    #endregion Public Properties


    #region Public Classes

    public class UpdateVideoCommandHandler : IRequestHandler<UpdateVideoCommand, Envelope<string>>
    {
        #region Private Fields

        private readonly IVideoUseCase _videoUseCase;

        #endregion Private Fields

        #region Public Constructors

        public UpdateVideoCommandHandler(IVideoUseCase videoUseCase)
        {
            _videoUseCase = videoUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<string>> Handle(UpdateVideoCommand request, CancellationToken cancellationToken)
        {
            return await _videoUseCase.EditVideo(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}
