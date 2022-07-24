using ElevateOTT.Application.Features.Content.Authors.Commands.DeleteAuthor;
using ElevateOTT.Application.Features.Content.Authors.Commands.CreateAuthor;
using ElevateOTT.Application.Features.Content.Authors.Commands.UpdateAuthor;
using ElevateOTT.Application.Features.Content.Authors.Queries.ExportAuthors;
using ElevateOTT.Application.Features.Content.Authors.Queries.GetAuthorForEdit;
using ElevateOTT.Application.Features.Content.Authors.Queries.GetAuthors;

namespace ElevateOTT.WebAPI.Controllers;

[Route("api/authors")]
[ApiController]
[BpAuthorize]
public class AuthorsController : ApiController
{
    //private readonly IMediator Mediator;

    //public AuthorsController(IMediator mediator)
    //{
    //    Mediator = mediator;
    //}

    #region Public Methods

    [HttpPost("Author")]
    public async Task<IActionResult> GetAuthor(GetAuthorForEditQuery request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("Authors")]
    public async Task<IActionResult> GetAuthors(GetAuthorsQuery request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAuthor([FromBody] UpdateAuthorCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAuthor(Guid id)
    {
        var response = await Mediator.Send(new DeleteAuthorCommand { Id = id });
        return TryGetResult(response);
    }

    [AllowAnonymous]
    [HttpPost("GetAuthorNoAuth")]
    public async Task<IActionResult> GetAuthorNoAuth(GetAuthorForEditQuery request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [AllowAnonymous]
    [HttpPost("GetAuthorsNoAuth")]
    public async Task<IActionResult> GetAuthorsNoAuth(GetAuthorsQuery request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [AllowAnonymous]
    [HttpPost("CreateAuthorNoAuth")]
    public async Task<IActionResult> CreateAuthorNoAuth(CreateAuthorCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [AllowAnonymous]
    [HttpPut("UpdateAuthorNoAuth")]
    public async Task<IActionResult> UpdateAuthorNoAuth(UpdateAuthorCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [AllowAnonymous]
    [HttpDelete("DeleteAuthorNoAuth")]
    public async Task<IActionResult> DeleteAuthorNoAuth(Guid id)
    {
        var response = await Mediator.Send(new DeleteAuthorCommand { Id = id });
        return TryGetResult(response);
    }

    [AllowAnonymous]
    [HttpPost("FluentValidation")]
    public async Task<IActionResult> FluentValidation(CreateAuthorCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }


    [AllowAnonymous]
    [HttpPost("ExportAsXls")]
    public async Task<IActionResult> ExportAsXls(ExportAuthorsQuery request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [AllowAnonymous]
    [HttpPost("ExportAsPdf")]
    public async Task<IActionResult> ExportAsPdf(ExportAuthorsQuery request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }
    #endregion Public Methods
}
