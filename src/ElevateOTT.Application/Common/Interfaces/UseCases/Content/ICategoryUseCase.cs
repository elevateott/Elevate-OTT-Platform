using ElevateOTT.Application.Features.Content.Categories.Commands.CreateCategory;
using ElevateOTT.Application.Features.Content.Categories.Commands.DeleteCategory;
using ElevateOTT.Application.Features.Content.Categories.Commands.UpdateCategory;
using ElevateOTT.Application.Features.Content.Categories.Queries.GetCategories;
using ElevateOTT.Application.Features.Content.Categories.Queries.GetCategoryForEdit;

namespace ElevateOTT.Application.Common.Interfaces.UseCases.Content;

public interface ICategoryUseCase
{
    #region Public Methods

    Task<Envelope<CategoryForEdit>> GetCategory(GetCategoryForEditQuery request);
    Task<Envelope<CategoriesResponse>> GetCategories(GetCategoriesQuery request);
    Task<Envelope<CreateCategoryResponse>> AddCategory(CreateCategoryCommand request);
    Task<Envelope<string>> EditCategory(UpdateCategoryCommand request);
    Task<Envelope<string>> DeleteCategory(DeleteCategoryCommand request);

    #endregion Public Methods
}
