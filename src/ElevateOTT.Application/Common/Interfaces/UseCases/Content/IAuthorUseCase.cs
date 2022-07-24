using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevateOTT.Application.Features.Content.Authors.Commands.CreateAuthor;
using ElevateOTT.Application.Features.Content.Authors.Commands.DeleteAuthor;
using ElevateOTT.Application.Features.Content.Authors.Commands.UpdateAuthor;
using ElevateOTT.Application.Features.Content.Authors.Queries.ExportAuthors;
using ElevateOTT.Application.Features.Content.Authors.Queries.GetAuthorForEdit;
using ElevateOTT.Application.Features.Content.Authors.Queries.GetAuthors;

namespace ElevateOTT.Application.Common.Interfaces.UseCases.Content;
public interface IAuthorUseCase
{
    #region Public Methods
    Task<Envelope<AuthorForEdit>> GetAuthor(GetAuthorForEditQuery request);
    Task<Envelope<AuthorsResponse>> GetAuthors(GetAuthorsQuery request);
    Task<Envelope<CreateAuthorResponse>> AddAuthor(CreateAuthorCommand request);
    Task<Envelope<string>> EditAuthor(UpdateAuthorCommand request);
    Task<Envelope<string>> DeleteAuthor(DeleteAuthorCommand request);
    Task<Envelope<ExportAuthorsResponse>> ExportAsPdf(ExportAuthorsQuery request);
    #endregion Public Methods
}
