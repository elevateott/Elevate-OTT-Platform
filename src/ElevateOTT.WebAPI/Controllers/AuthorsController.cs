using ElevateOTT.Application.Features.Content.Authors.Commands.CreateAuthor;
using ElevateOTT.Application.Features.Content.Authors.Commands.DeleteAuthor;
using ElevateOTT.Application.Features.Content.Authors.Commands.UpdateAuthor;
using ElevateOTT.Application.Features.Content.Authors.Queries.GetAuthorForEdit;
using ElevateOTT.Application.Features.Content.Authors.Queries.GetAuthors;
using ElevateOTT.Application.Features.Content.Authors.Queries.GetAuthorsForAutoComplete;

namespace ElevateOTT.WebAPI.Controllers;

//[BpAuthorize]
[AllowAnonymous]
[Route("api/authors")]
[ApiController]
public class AuthorsController : ApiController
{
    #region Public Methods

    [HttpGet("{id:guid}", Name = "AuthorById")]
    public async Task<IActionResult> GetAuthor(Guid id)
    {
        var httpRequest = Request;
        var response = await Mediator.Send(new GetAuthorForEditQuery {Id = id});
        return TryGetResult(response);
    }

    //[AutoWrapIgnore]
    [HttpPost]
    public async Task<IActionResult> GetAuthors([FromBody] GetAuthorsQuery request)
    {
        var httpRequest = Request;
        var response = await Mediator.Send(request);

        return TryGetResult(response);
    }

    [HttpPost("auto-complete")]
    public async Task<IActionResult> GetAuthorsForAutoComplete([FromBody] GetAuthorsForAutoCompleteQuery request)
    {
        var httpRequest = Request;
        var response = await Mediator.Send(request);

        return TryGetResult(response);
    }

    [HttpPost("add")]
    [RequestFormLimits(MultipartBodyLengthLimit = 20971520)] // 20MB
    public async Task<IActionResult> CreateAuthor([FromForm] CreateAuthorCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    //[HttpPost]
    //public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorCommand request)
    //{
    //    var response = await Mediator.Send(request);
    //    return TryGetResult(response);
    //}


    [HttpPut]
    public async Task<IActionResult> UpdateAuthor([FromForm] UpdateAuthorCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    //[HttpPut]
    //public async Task<IActionResult> UpdateAuthor([FromBody] UpdateAuthorCommand request)
    //{
    //    var response = await Mediator.Send(request);
    //    return TryGetResult(response);
    //}

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAuthor(Guid id)
    {
        var response = await Mediator.Send(new DeleteAuthorCommand { Id = id });
        return TryGetResult(response);
    }

    #endregion Public Methods
}
