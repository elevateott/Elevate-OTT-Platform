using ElevateOTT.ClientPortal.Features.Content.Categories.Queries.GetCategories;
using ElevateOTT.ClientPortal.Features.Content.Categories.Queries.GetCategoriesForAutoComplete;

namespace ElevateOTT.ClientPortal.Interfaces.Consumers;

public interface ICategoriesClient
{
    #region Public Methods

    Task<HttpResponseWrapper<object>> GetCategory(Guid id);
    Task<HttpResponseWrapper<object>> GetCategories(GetCategoriesQuery request);
    Task<HttpResponseWrapper<object>> GetCategoriesForAutoComplete(GetCategoriesForAutoCompleteQuery request);
    Task<HttpResponseWrapper<object>> CreateCategory(MultipartFormDataContent request);
    Task<HttpResponseWrapper<object>> UpdateCategory(MultipartFormDataContent request);
    Task<HttpResponseWrapper<object>> DeleteCategory(Guid id);

    #endregion Public Methods
}
