using ElevateOTT.Application.Common.Interfaces.UseCases.Content;

namespace ElevateOTT.Application.Features.Content.Videos.Commands.DeleteVideo;

public class DeleteVideoCommand : IRequest<Envelope<string>>
{
    #region Public Properties

    public Guid Id { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class DeleteVideoCommandHandler : IRequestHandler<DeleteVideoCommand, Envelope<string>>
    {
        #region Private Fields

        private readonly IVideoUseCase _videoUseCase;

        #endregion Private Fields

        #region Public Constructors

        public DeleteVideoCommandHandler(IVideoUseCase videoUseCase)
        {
            _videoUseCase = videoUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<string>> Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
        {
            return await _videoUseCase.DeleteVideo(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}
