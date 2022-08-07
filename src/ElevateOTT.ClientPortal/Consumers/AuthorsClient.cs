using ElevateOTT.ClientPortal.Features.Content.Authors.Commands.CreateAuthor;
using ElevateOTT.ClientPortal.Features.Content.Authors.Commands.UpdateAuthor;
using ElevateOTT.ClientPortal.Features.Content.Authors.Queries.GetAuthorForEdit;
using ElevateOTT.ClientPortal.Features.Content.Authors.Queries.GetAuthors;

namespace ElevateOTT.ClientPortal.Consumers;

public class AuthorsClient : IAuthorsClient
{
    #region Private Fields

    private readonly IHttpService _httpService;
    private const string ControllerName = "authors";

    #endregion Private Fields

    #region Public Constructors

    public AuthorsClient(IHttpService httpService)
    {
        _httpService = httpService;
    }

    #endregion Public Constructors

    public async Task<HttpResponseWrapper<object>> GetAuthor(Guid id)
    {
        return await _httpService.Get<AuthorForEdit>($"{ControllerName}/{id}");
    }

    public async Task<HttpResponseWrapper<object>> GetAuthors(GetAuthorsQuery request)
    {
        return await _httpService.Post<GetAuthorsQuery, AuthorsResponse>($"{ControllerName}", request);
    }

    public async Task<HttpResponseWrapper<object>> CreateAuthor(MultipartFormDataContent request)
    {
        return await _httpService.PostFormData<MultipartFormDataContent, CreateAuthorResponse>($"{ControllerName}/add", request);
    }

    //public async Task<HttpResponseWrapper<object>> CreateAuthor(CreateAuthorCommand request)
    //{
    //    return await _httpService.Post<CreateAuthorCommand, CreateAuthorResponse>("authors", request);
    //}

    public async Task<HttpResponseWrapper<object>> UpdateAuthor(MultipartFormDataContent request)
    {
        Console.WriteLine("UpdateAuthor invoked");
        Console.WriteLine("request: " + request);
        return await _httpService.PostFormData<MultipartFormDataContent, string>($"{ControllerName}", request);
    }

    //public async Task<HttpResponseWrapper<object>> UpdateAuthor(UpdateAuthorCommand request)
    //{
    //    return await _httpService.Put<UpdateAuthorCommand, string>("authors", request);
    //}

    public async Task<HttpResponseWrapper<object>> DeleteAuthor(Guid id)
    {
        return await _httpService.Delete<string>($"{ControllerName}/{id}");
    }
}
