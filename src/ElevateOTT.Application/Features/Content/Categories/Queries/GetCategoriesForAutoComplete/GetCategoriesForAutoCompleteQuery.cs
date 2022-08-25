using ElevateOTT.Application.Common.Interfaces.UseCases.Content;
using ElevateOTT.Application.Features.Content.Categories.Queries.GetCategories;

namespace ElevateOTT.Application.Features.Content.Categories.Queries.GetCategoriesForAutoComplete;

public class GetCategoriesForAutoCompleteQuery : FilterableQuery, IRequest<Envelope<CategoriesForAutoCompleteResponse>>
{
    #region Public Classes
    public class GetCategoriesForAutoCompleteQueryHandler : IRequestHandler<GetCategoriesForAutoCompleteQuery, Envelope<CategoriesForAutoCompleteResponse>>
    {
        #region Private Fields

        private readonly ICategoryUseCase _categoryUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetCategoriesForAutoCompleteQueryHandler(ICategoryUseCase categoryUseCase)
        {
            _categoryUseCase = categoryUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<CategoriesForAutoCompleteResponse>> Handle(GetCategoriesForAutoCompleteQuery request, CancellationToken cancellationToken)
        {
            var response = await _categoryUseCase.GetCategories(new GetCategoriesQuery
            {
                PageSize = request.PageSize,
                SearchText = request.SearchText
            });

            if (response?.Payload?.Categories?.Items is null)
            {
                return Envelope<CategoriesForAutoCompleteResponse>.Result.NotFound();
            }

            var responseForAutoComplete = new CategoriesForAutoCompleteResponse
            {
                Categories = new PagedList<CategoryItemForAutoComplete>()
            };

            foreach (var category in response.Payload.Categories.Items)
            {
                responseForAutoComplete.Categories.Items.Add(new CategoryItemForAutoComplete
                {
                    Id = category.Id,
                    Title = category.Title,
                    ImageUrl = category.ImageUrl ?? string.Empty
                });
            }

            return Envelope<CategoriesForAutoCompleteResponse>.Result.Ok(responseForAutoComplete);
        }

        #endregion Public Methods

    }

    #endregion Public Classes
}
