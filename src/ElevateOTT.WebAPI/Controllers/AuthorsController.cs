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
    #region Public Methods
    [HttpGet("author/{id:guid}", Name = "AuthorById")]
    public async Task<IActionResult> GetAuthor(Guid id)
    {
        var response = await Mediator.Send(new GetAuthorForEditQuery {Id = id});
        return TryGetResult(response);
    }

    [HttpPost("authors")]
    public async Task<IActionResult> GetAuthors([FromBody] GetAuthorsQuery request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("new-author")]
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

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAuthor(Guid id)
    {
        var response = await Mediator.Send(new DeleteAuthorCommand { Id = id });
        return TryGetResult(response);
    }
    #endregion Public Methods
}
