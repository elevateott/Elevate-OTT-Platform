using ElevateOTT.Application.Features.Content.Videos.Commands.CreateAssetAtMux;
using ElevateOTT.Application.Features.Content.Videos.Commands.CreateVideo;
using ElevateOTT.Application.Features.Content.Videos.Commands.DeleteVideo;
using ElevateOTT.Application.Features.Content.Videos.Commands.UpdateVideo;
using ElevateOTT.Application.Features.Content.Videos.Queries.GetNewStorageName;
using ElevateOTT.Application.Features.Content.Videos.Queries.GetSasToken;
using ElevateOTT.Application.Features.Content.Videos.Queries.GetVideoForEdit;
using ElevateOTT.Application.Features.Content.Videos.Queries.GetVideos;
using ElevateOTT.Application.Features.Identity.Tenants.Queries;

namespace ElevateOTT.WebAPI.Controllers;

//[BpAuthorize]
[Route("api/videos")]
[ApiController]
public class VideosController : ApiController
{
    #region Public Methods

    [HttpGet("{id:guid}", Name = "VideoById")]
    public async Task<IActionResult> GetVideo(Guid id)
    {
        var httpRequest = Request;
        var response = await Mediator.Send(new GetVideoForEditQuery { Id = id });
        return TryGetResult(response);
    }

    [HttpGet("azure-blob-sas-token")]
    public async Task<IActionResult> GetSasToken()
    {
        var response = await Mediator.Send(new GetSasTokenQuery());
        return TryGetResult(response);
    }

    [HttpGet("new-storage-name")]
    public async Task<IActionResult> GetNewVideoStorageName()
    {
        var response = await Mediator.Send(new GetNewStorageNameQuery());
        return TryGetResult(response);
    }

    //[AutoWrapIgnore]
    [HttpPost]
    public async Task<IActionResult> GetVideos([FromBody] GetVideosQuery request)
    {
        var httpRequest = Request;
        var response = await Mediator.Send(request);
        return TryGetResult(response);

    }

    //[HttpPost]
    //[RequestFormLimits(MultipartBodyLengthLimit = 20971520)] // 20MB
    //public async Task<IActionResult> CreateVideo([FromForm] CreateVideoCommand request)
    //{
    //    var response = await Mediator.Send(request);
    //    return TryGetResult(response);
    //}

    [HttpPost("add")]
    public async Task<IActionResult> CreateVideo([FromBody] CreateVideoCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("add-asset-at-mux")]
    public async Task<IActionResult> CreateAssetAtMux([FromBody] CreateAssetAtMuxCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }
    

    [HttpPut]
    public async Task<IActionResult> UpdateVideo([FromForm] UpdateVideoCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    //[HttpPut]
    //public async Task<IActionResult> UpdateVideo([FromBody] UpdateVideoCommand request)
    //{
    //    var response = await Mediator.Send(request);
    //    return TryGetResult(response);
    //}

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteVideo(Guid id)
    {
        var response = await Mediator.Send(new DeleteVideoCommand { Id = id });
        return TryGetResult(response);
    }

    #endregion Public Methods
}
