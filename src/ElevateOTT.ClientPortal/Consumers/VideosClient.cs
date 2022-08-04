using ElevateOTT.ClientPortal.Features.Content.Videos.Commands.CreateVideo;
using ElevateOTT.ClientPortal.Features.Content.Videos.Commands.UpdateVideo;
using ElevateOTT.ClientPortal.Features.Content.Videos.Queries.GetVideoForEdit;
using ElevateOTT.ClientPortal.Features.Content.Videos.Queries.GetVideos;

namespace ElevateOTT.ClientPortal.Consumers;

public class VideosClient : IVideosClient
{
    #region Private Fields

    private readonly IHttpService _httpService;

    #endregion Private Fields

    #region Public Constructors

    public VideosClient(IHttpService httpService)
    {
        _httpService = httpService;
    }

    #endregion Public Constructors

    public async Task<HttpResponseWrapper<object>> GetVideo(Guid id)
    {
        return await _httpService.Get<VideoForEdit>($"videos/video/{id}");
    }

    public async Task<HttpResponseWrapper<object>> GetVideos(GetVideosQuery request)
    {
        return await _httpService.Post<GetVideosQuery, VideosResponse>("videos/videos", request);
    }

    public async Task<HttpResponseWrapper<object>> CreateVideoFormData(MultipartFormDataContent request)
    {
        return await _httpService.PostFormData<MultipartFormDataContent, CreateVideoResponse>("videos/multipart-form-create", request);
    }

    public async Task<HttpResponseWrapper<object>> CreateVideo(CreateVideoCommand request)
    {
        return await _httpService.Post<CreateVideoCommand, CreateVideoResponse>("videos/new-video", request);
    }

    public async Task<HttpResponseWrapper<object>> UpdateVideoFormData(MultipartFormDataContent request)
    {
        Console.WriteLine("UpdateVideoFormData invoked");
        Console.WriteLine("request: " + request);
        return await _httpService.PostFormData<MultipartFormDataContent, string>("videos/multipart-form-update", request);
    }

    public async Task<HttpResponseWrapper<object>> UpdateVideo(UpdateVideoCommand request)
    {
        return await _httpService.Put<UpdateVideoCommand, string>("videos", request);
    }

    public async Task<HttpResponseWrapper<object>> DeleteVideo(Guid id)
    {
        return await _httpService.Delete<string>($"videos/{id}");
    }
}
