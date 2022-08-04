using ElevateOTT.Application.Features.Content.Videos.Commands.CreateVideo;
using ElevateOTT.Application.Features.Content.Videos.Commands.DeleteVideo;
using ElevateOTT.Application.Features.Content.Videos.Commands.UpdateVideo;
using ElevateOTT.Application.Features.Content.Videos.Queries.GetVideoForEdit;
using ElevateOTT.Application.Features.Content.Videos.Queries.GetVideos;

namespace ElevateOTT.WebAPI.Controllers;

[Route("api/videos")]
[ApiController]
[BpAuthorize]
public class VideosController : ApiController
{
    #region Public Methods

    [HttpGet("video/{id:guid}", Name = "VideoById")]
    [AllowAnonymous]
    public async Task<IActionResult> GetVideo(Guid id)
    {
        var httpRequest = Request;
        var response = await Mediator.Send(new GetVideoForEditQuery { Id = id });
        return TryGetResult(response);
    }

    //[AutoWrapIgnore]
    [HttpPost("videos")]
    [AllowAnonymous]
    public async Task<IActionResult> GetVideos([FromBody] GetVideosQuery request)
    {
        var httpRequest = Request;
        var response = await Mediator.Send(request);
        var result = TryGetResult(response);

        return result;
    }

    [HttpPost("multipart-form-create")]
    [RequestFormLimits(MultipartBodyLengthLimit = 20971520)] // 20MB
    public async Task<IActionResult> CreateVideoByMultipartForm([FromForm] CreateVideoCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [AllowAnonymous]
    [HttpPost("new-video")]
    public async Task<IActionResult> CreateVideo([FromBody] CreateVideoCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }


    [AllowAnonymous]
    [HttpPost("multipart-form-update")]
    public async Task<IActionResult> UpdateVideoByMultipartForm([FromForm] UpdateVideoCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateVideo([FromBody] UpdateVideoCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteVideo(Guid id)
    {
        var response = await Mediator.Send(new DeleteVideoCommand { Id = id });
        return TryGetResult(response);
    }

    #endregion Public Methods
}
