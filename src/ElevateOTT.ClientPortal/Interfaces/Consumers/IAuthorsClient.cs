using ElevateOTT.ClientPortal.Features.Content.Authors.Commands.CreateAuthor;
using ElevateOTT.ClientPortal.Features.Content.Authors.Commands.UpdateAuthor;
using ElevateOTT.ClientPortal.Features.Content.Authors.Queries.GetAuthorForEdit;
using ElevateOTT.ClientPortal.Features.Content.Authors.Queries.GetAuthors;
using ElevateOTT.ClientPortal.Features.Content.Authors.Queries.GetAuthorsForAutoComplete;

namespace ElevateOTT.ClientPortal.Interfaces.Consumers;

public interface IAuthorsClient
{
    #region Public Methods

    Task<HttpResponseWrapper<object>> GetAuthor(Guid id);
    Task<HttpResponseWrapper<object>> GetAuthors(GetAuthorsQuery request);
    Task<HttpResponseWrapper<object>> GetAuthorsForAutoComplete(GetAuthorsForAutoCompleteQuery request);
    Task<HttpResponseWrapper<object>> CreateAuthor(MultipartFormDataContent request);
    //Task<HttpResponseWrapper<object>> CreateAuthor(CreateAuthorCommand request);
    Task<HttpResponseWrapper<object>> UpdateAuthor(MultipartFormDataContent request);
    //Task<HttpResponseWrapper<object>> UpdateAuthor(UpdateAuthorCommand request);
    Task<HttpResponseWrapper<object>> DeleteAuthor(Guid id);

    #endregion Public Methods
}
