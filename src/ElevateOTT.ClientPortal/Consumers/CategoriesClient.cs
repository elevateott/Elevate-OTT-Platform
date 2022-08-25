using ElevateOTT.ClientPortal.Features.Content.Categories.Commands.CreateCategory;
using ElevateOTT.ClientPortal.Features.Content.Categories.Queries.GetCategories;
using ElevateOTT.ClientPortal.Features.Content.Categories.Queries.GetCategoriesForAutoComplete;
using ElevateOTT.ClientPortal.Features.Content.Categories.Queries.GetCategoryForEdit;

namespace ElevateOTT.ClientPortal.Consumers;

public class CategoriesClient : ICategoriesClient
{
    #region Private Fields

    private readonly IHttpService _httpService;
    private const string ControllerName = "categories";

    #endregion Private Fields

    #region Public Constructors

    public CategoriesClient(IHttpService httpService)
    {
        _httpService = httpService;
    }

    #endregion Public Constructors

    public async Task<HttpResponseWrapper<object>> GetCategory(Guid id)
    {
        return await _httpService.Get<CategoryForEdit>($"{ControllerName}/{id}");
    }

    public async Task<HttpResponseWrapper<object>> GetCategories(GetCategoriesQuery request)
    {
        return await _httpService.Post<GetCategoriesQuery, CategoriesResponse>($"{ControllerName}", request);
    }

    public async Task<HttpResponseWrapper<object>> GetCategoriesForAutoComplete(GetCategoriesForAutoCompleteQuery request)
    {
        return await _httpService.Post<GetCategoriesForAutoCompleteQuery, CategoriesForAutoCompleteResponse>($"{ControllerName}/auto-complete", request);
    }

    public async Task<HttpResponseWrapper<object>> CreateCategory(MultipartFormDataContent request)
    {
        return await _httpService.PostFormData<MultipartFormDataContent, CreateCategoryResponse>($"{ControllerName}/add", request);
    }

    public async Task<HttpResponseWrapper<object>> UpdateCategory(MultipartFormDataContent request)
    {
        Console.WriteLine("UpdateCategory invoked");
        Console.WriteLine("request: " + request);
        return await _httpService.PostFormData<MultipartFormDataContent, string>($"{ControllerName}", request);
    }

    public async Task<HttpResponseWrapper<object>> DeleteCategory(Guid id)
    {
        return await _httpService.Delete<string>($"{ControllerName}/{id}");
    }
}
