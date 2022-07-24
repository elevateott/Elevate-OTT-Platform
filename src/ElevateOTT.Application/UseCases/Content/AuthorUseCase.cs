using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevateOTT.Application.Common.Interfaces.Repository;
using ElevateOTT.Application.Common.Interfaces.UseCases.Content;
using ElevateOTT.Application.Features.Content.Authors.Commands.CreateAuthor;
using ElevateOTT.Application.Features.Content.Authors.Commands.DeleteAuthor;
using ElevateOTT.Application.Features.Content.Authors.Commands.UpdateAuthor;
using ElevateOTT.Application.Features.Content.Authors.Queries.ExportAuthors;
using ElevateOTT.Application.Features.Content.Authors.Queries.GetAuthorForEdit;
using ElevateOTT.Application.Features.Content.Authors.Queries.GetAuthors;
using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Application.UseCases.Content;
public class AuthorUseCase : IAuthorUseCase
{
    #region Private Fields

    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IAuthorRepository _authorRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IReportingService _reportingService;
    private readonly ITenantResolver _tenantResolver;

    #endregion Private Fields

    #region Public Constructors

    public AuthorUseCase(IApplicationDbContext dbContext,
                            IHttpContextAccessor httpContextAccessor,
                            IReportingService reportingService, IMapper mapper, IAuthorRepository authorRepository, ITenantResolver tenantResolver)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
        _reportingService = reportingService;
        _mapper = mapper;
        _authorRepository = authorRepository;
        _tenantResolver = tenantResolver;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<Envelope<AuthorForEdit>> GetAuthor(GetAuthorForEditQuery request)
    {
        if (request.Id is null)
        {
            return Envelope<AuthorForEdit>.Result.BadRequest(Resource.Invalid_author_Id);
        }

        var tenantId = _tenantResolver.GetTenantId();

        if (tenantId is null)
        {
            return Envelope<AuthorForEdit>.Result.BadRequest(Resource.Invalid_tenant_Id);
        }

        var author = await _authorRepository.GetAuthorAsync(tenantId.Value, request.Id.Value, false);

        if (author == null)
            return Envelope<AuthorForEdit>.Result.NotFound(Resource.Unable_to_load_author);

        var authorForEdit = _mapper.Map<AuthorForEdit>(author);

        return Envelope<AuthorForEdit>.Result.Ok(authorForEdit);
    }

    public async Task<Envelope<AuthorsResponse>> GetAuthors(GetAuthorsQuery request)
    {
        var tenantId = _tenantResolver.GetTenantId();

        if (tenantId is null)
        {
            return Envelope<AuthorsResponse>.Result.BadRequest(Resource.Invalid_tenant_Id);
        }

        var authors = await  _authorRepository.GetAuthorsAsync(tenantId.Value, request, false);


        var authorsResponse = new AuthorsResponse
        {
            Authors = _mapper.Map<PagedList<AuthorDto>>(authors)
        };

        return Envelope<AuthorsResponse>.Result.Ok(authorsResponse);
    }

    public async Task<Envelope<CreateAuthorResponse>> AddAuthor(CreateAuthorCommand request)
    {
        var tenantId = _tenantResolver.GetTenantId();

        if (tenantId is null)
        {
            return Envelope<CreateAuthorResponse>.Result.BadRequest(Resource.Invalid_tenant_Id);
        }

        var author = _mapper.Map<AuthorModel>(request);

        await _authorRepository.CreateAuthorForTenant(tenantId.Value, author);

        var createAuthorResponse = new CreateAuthorResponse
        {
            Id = author.Id,
            SuccessMessage = Resource.Author_has_been_created_successfully
        };

        return Envelope<CreateAuthorResponse>.Result.Ok(createAuthorResponse);
    }

    public async Task<Envelope<string>> EditAuthor(UpdateAuthorCommand request)
    {
        var tenantId = _tenantResolver.GetTenantId();

        if (tenantId is null)
        {
            return Envelope<string>.Result.BadRequest(Resource.Invalid_tenant_Id);
        }

        var authorEntity = await _authorRepository.GetAuthorAsync(tenantId.Value, request.Id, true);

        if (authorEntity == null)
            return Envelope<string>.Result.NotFound(Resource.Unable_to_load_author);

        _mapper.Map(request, authorEntity);

        await _dbContext.SaveChangesAsync();

        return Envelope<string>.Result.Ok(Resource.Author_has_been_updated_successfully);
    }

    public async Task<Envelope<string>> DeleteAuthor(DeleteAuthorCommand request)
    {
        var tenantId = _tenantResolver.GetTenantId();

        if (tenantId is null)
        {
            return Envelope<string>.Result.BadRequest(Resource.Invalid_tenant_Id);
        }

        var authorEntity = await _authorRepository.GetAuthorAsync(tenantId.Value, request.Id, true);

        if (authorEntity == null)
            return Envelope<string>.Result.NotFound(Resource.The_author_is_not_found);

        _authorRepository.DeleteAuthor(authorEntity);

        return Envelope<string>.Result.Ok(Resource.Author_has_been_deleted_successfully);
    }

    public Task<Envelope<ExportAuthorsResponse>> ExportAsPdf(ExportAuthorsQuery request)
    {
        throw new NotImplementedException();
    }

    //public async Task<Envelope<AuthorReferencesResponse>> GetAuthorReferences(GetAuthorReferencesQuery request)
    //{
    //    var tenantId = _tenantResolver.GetTenantId();


    //    var query = _dbContext.References.Where(a => a.AuthorId == request.AuthorId
    //                                                 && (a.Name.Contains(request.SearchText)
    //                                                 || a.JobTitle.Contains(request.SearchText)
    //                                                 || a.Phone.Contains(request.SearchText)
    //                                                 || string.IsNullOrEmpty(request.SearchText)));

    //    query = !string.IsNullOrWhiteSpace(request.SortBy)
    //        ? query.SortBy(request.SortBy)
    //        : query.OrderBy(a => a.Name);

    //    var authorReferenceItems = await query.Select(q => AuthorReferenceItem.MapFromEntity(q)).ToPagedListAsync(request.PageNumber, request.RowsPerPage);

    //    var authorReferencesResponse = new AuthorReferencesResponse
    //    {
    //        AuthorReferences = authorReferenceItems
    //    };

    //    return Envelope<AuthorReferencesResponse>.Result.Ok(authorReferencesResponse);
    //}

    //public async Task<Envelope<ExportAuthorsResponse>> ExportAsPdf(ExportAuthorsQuery request)
    //{
    //    //await Task.Delay(5000);
    //    //var path = _dataExportService.CreateSpreadsheetWorkbook(@"TestReport.xlsx");
    //    var authorResponse = await GetAuthors(new GetAuthorsQuery { SearchText = request.SearchText, SortBy = request.SortBy });

    //    var issuerName = (await _httpContextAccessor.GetUserNameAsync()).Split("@")[0];

    //    var host = $"{_httpContextAccessor.HttpContext?.Request.Scheme}://{_httpContextAccessor.HttpContext?.Request.Host}";

    //    var payload = await _reportingService.ExportAuthorsAsPdfImmediate(authorResponse.Payload, host, issuerName);

    //    return Envelope<ExportAuthorsResponse>.Result.Ok(payload);
    //}

    #endregion Public Methods
}
