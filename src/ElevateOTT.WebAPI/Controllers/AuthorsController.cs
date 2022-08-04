using ElevateOTT.Application.Features.Content.Videos.Commands.CreateVideo;
using ElevateOTT.Application.Features.Content.Videos.Commands.DeleteVideo;
using ElevateOTT.Application.Features.Content.Videos.Commands.UpdateVideo;
using ElevateOTT.Application.Features.Content.Videos.Queries.GetVideoForEdit;
using ElevateOTT.Application.Features.Content.Videos.Queries.GetVideos;

namespace ElevateOTT.WebAPI.Controllers;

[Route("api/authors")]
[ApiController]
[BpAuthorize]
public class AuthorsController : ApiController
{
    #region Public Methods

    [HttpGet("author/{id:guid}", Name = "AuthorById")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAuthor(Guid id)
    {
        var httpRequest = Request;
        var response = await Mediator.Send(new GetVideoForEditQuery {Id = id});
        return TryGetResult(response);
    }

    //[AutoWrapIgnore]
    [HttpPost("authors")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAuthors([FromBody] GetVideosQuery request)
    {
        var httpRequest = Request;
        var response = await Mediator.Send(request);
        var result =  TryGetResult(response);

        return result;
    }

    [HttpPost("multipart-form-create")]
    [RequestFormLimits(MultipartBodyLengthLimit = 20971520)] // 20MB
    public async Task<IActionResult> CreateAuthorByMultipartForm([FromForm] CreateVideoCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("new-author")]
    public async Task<IActionResult> CreateAuthor([FromBody] CreateVideoCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }


    [AllowAnonymous]
    [HttpPost("multipart-form-update")]
    public async Task<IActionResult> UpdateAuthorByMultipartForm([FromForm] UpdateVideoCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAuthor([FromBody] UpdateVideoCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAuthor(Guid id)
    {
        var response = await Mediator.Send(new DeleteVideoCommand { Id = id });
        return TryGetResult(response);
    }

    #endregion Public Methods
}
