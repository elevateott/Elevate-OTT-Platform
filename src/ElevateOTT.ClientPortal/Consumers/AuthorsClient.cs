﻿using ElevateOTT.ClientPortal.Features.Content.Authors.Commands.CreateAuthor;
using ElevateOTT.ClientPortal.Features.Content.Authors.Commands.UpdateAuthor;
using ElevateOTT.ClientPortal.Features.Content.Authors.Queries.GetAuthorForEdit;
using ElevateOTT.ClientPortal.Features.Content.Authors.Queries.GetAuthors;

namespace ElevateOTT.ClientPortal.Consumers;

public class AuthorsClient : IAuthorsClient
{
    #region Private Fields
    private readonly IHttpService _httpService;
    #endregion Private Fields

    #region Public Constructors
    public AuthorsClient(IHttpService httpService)
    {
        _httpService = httpService;
    }
    #endregion Public Constructors

    public async Task<HttpResponseWrapper<object>> GetAuthor(GetAuthorForEditQuery request)
    {
        return await _httpService.Post<GetAuthorForEditQuery, AuthorForEdit>("authors/author", request);
    }

    public async Task<HttpResponseWrapper<object>> GetAuthors(GetAuthorsQuery request)
    {
        return await _httpService.Post<GetAuthorsQuery, AuthorsResponse>("authors/authors", request);
    }

    public async Task<HttpResponseWrapper<object>> CreateAuthor(CreateAuthorCommand request)
    {
        return await _httpService.Post<CreateAuthorCommand, CreateAuthorResponse>("authors/new-author", request);
    }

    public async Task<HttpResponseWrapper<object>> UpdateAuthor(UpdateAuthorCommand request)
    {
        return await _httpService.Put<UpdateAuthorCommand, string>("authors", request);
    }

    public async Task<HttpResponseWrapper<object>> DeleteAuthor(Guid id)
    {
        return await _httpService.Delete<string>($"authors/{id}");
    }
}
