using ElevateOTT.Application.Common.Interfaces.UseCases.Content;

namespace ElevateOTT.Application.Features.Content.Authors.Commands.CreateAuthor;

public class CreateAuthorCommand : IRequest<Envelope<CreateAuthorResponse>>
{
    #region Public Properties
    public string Name { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string SeoTitle { get; set; } = string.Empty;
    public string SeoDescription { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;

    #endregion Public Properties


    #region Public Classes
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Envelope<CreateAuthorResponse>>
    {
        #region Private Fields
        private readonly IAuthorUseCase _authorUseCase;
        #endregion Private Fields

        #region Public Constructors
        public CreateAuthorCommandHandler(IAuthorUseCase authorUseCase)
        {
            _authorUseCase = authorUseCase;
        }
        #endregion Public Constructors

        #region Public Methods
        public async Task<Envelope<CreateAuthorResponse>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            return await _authorUseCase.AddAuthor(request);
        }
        #endregion Public Methods
    }
    #endregion Public Classes
}
